using Cargold.PoolingSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using Cargold;

public class ItemPooling_Script : MonoBehaviour, IPooler
{
    [SerializeField, LabelText("������ ��������Ʈ ������")] private SpriteRenderer _itemSpriteRenderer;
    [SerializeField, LabelText("������ٵ�")] private Rigidbody2D _itemRid;
    
    [SerializeField, LabelText("���� ����Ʈ(�ӽ�)")] private Transform _itemCheckPointTr;

    [SerializeField, LabelText("���� ������")] private Vector3 _itemScale;

    [LabelText("���� ���� ������")] private Item_InfoData _myData;

    public void InitializedByPoolingSystem()
    {
        this.InIt_Func();
    }

    private void InIt_Func()
    {

    }

    public void Setting_Func(Item_InfoData a_ItemData, Vector2 a_SpawnPos, bool is_Up)
    {
        this._myData = a_ItemData;

        this.gameObject.SetActive(true);
        this.transform.SetParent(InGameUISystem_Manager.Instance.itemSpawnPoint);
        this.transform.position = a_SpawnPos;
        this.transform.localScale = _itemScale;

        //������ �̹��� ����
        this._itemSpriteRenderer.sprite = ItemSystem_Manager.Instance.Get_ItemIntKeyToSprite_Func(this._myData.IntKey);

        //������ ������ٵ� �� �߰�
        this._itemRid.mass = a_ItemData.Mass;
        this._itemRid.gravityScale = DataBase_Manager.Instance.GetTable_Define.item_Falling_Speed;

        if(is_Up == true)
        {
            //������ ��¦ ���� �ö󰡾���.
            Vector2 a_StartMoveVec = Vector2.zero;
            a_StartMoveVec.x = Random.Range(-3, 4);
            a_StartMoveVec.y = 5;

            this._itemRid.velocity = a_StartMoveVec;
        }
    }

    private void Delete_Func(bool is_Player = false)
    {
        PoolingSystem_Manager.Instance.Despawn_Func(PoolingKey.ItempPoolingKey, this);
        this.gameObject.SetActive(false);

        if(is_Player == true)
        {
            InGameUiAnim_Script.Instance.Call_ItemAnim_Func(this._myData.IntKey);

            UserSystem_Manager.Instance.playInfo.Set_ScorePlayInfo_Func(this._myData.ItemScore);
            EnemySystem_Manager.Instance.Check_CurGetScoreToItemCoolTimeDown_Func(this._myData.ItemScore);
            EnemySystem_Manager.Instance.Check_EnemySpawnCondition_Func();

            //true�� ���Դٴ� �� ĳġ�� �������� ����ٴ� ��.
            //�׷� �����۰� �÷��̾� ĳġ �κ��� ��ħ
            this.Check_PositionDistance_Func();
        }
        else
        {
            UserSystem_Manager.Instance.playInfo.Set_SmashedScorePlayInfo_Func(this._myData.ItemScore);
            UserSystem_Manager.Instance.playInfo.Set_SmashedItemCountPlayInfo_Func();

            //ȿ����
            SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.��������ȿ����);

            bool a_Random = Random.Range(0, 2) == 0 ? true : false;

            if(a_Random == true)
            {
                SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.�Ҹ�1);
            }
            else
            {
                SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.�Ҹ�2);
            }

            //�÷��̾� ĳġ ���� UI ���
        }

        InGameUISystem_Manager.Instance.Score_Update_Func();
        InGameUISystem_Manager.Instance.SmashedScore_Update_Func();
        ItemSystem_Manager.Instance.Set_CountDown_Func();
    }

    private void Check_PositionDistance_Func()
    {
        float a_Distance = 0.0f;
        a_Distance = Vector2.Distance(this.transform.position, PlayerSystem_Manager.Instance.playerMoveScript.Get_ChtchPointTr_Func().position);

        if(a_Distance <= DataBase_Manager.Instance.GetTable_Define.chtch_PerpectPersent)
        {
            //����Ʈ UI ���
            SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.����Ʈ);
        }
        else
        {
            //�� UI ���
            SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.��);
        }

    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Guard")
        {
            //���� �ε����� ��� ����ұ�?
        }
        else if(coll.gameObject.tag == "DownGuard")
        {
            this.Delete_Func();
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "CatchPoint")
        {
            this.Delete_Func(true);
        }
    }
}
