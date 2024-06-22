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

    public void Setting_Func(Item_InfoData a_ItemData, Vector2 a_SpawnPos)
    {
        this._myData = a_ItemData;

        this.gameObject.SetActive(true);
        this.transform.SetParent(InGameUISystem_Manager.Instance.itemSpawnPoint);
        this.transform.position = a_SpawnPos;
        this.transform.localScale = _itemScale;

        //this._itemCheckPointTr = 

        //������ �̹��� ����
        this._itemSpriteRenderer.sprite = this._myData.Icon;

        //������ ������ٵ� �� �߰�
        this._itemRid.mass = a_ItemData.Mass;
        this._itemRid.gravityScale = DataBase_Manager.Instance.GetTable_Define.item_Falling_Speed;

        //������ ��¦ ���� �ö󰡾���.
        Vector2 a_StartMoveVec = Vector2.zero;
        a_StartMoveVec.x = Random.Range(-3, 4);
        a_StartMoveVec.y = 5;

        this._itemRid.velocity = a_StartMoveVec;
    }

    private void Delete_Func(bool is_Player = false)
    {
        PoolingSystem_Manager.Instance.Despawn_Func(PoolingKey.ItempPoolingKey, this);
        this.gameObject.SetActive(false);

        if(is_Player == true)
        {
            InGameUiAnim_Script.Instance.Call_ItemAnim_Func(this._myData.IntKey);

            UserSystem_Manager.Instance.playInfo.Set_ScorePlayInfo_Func(this._myData.ItemScore);
            EnemySystem_Manager.Instance.Check_EnemySpawnCondition_Func();

            //true�� ���Դٴ� �� ĳġ�� �������� ����ٴ� ��.
            //�׷� �����۰� �÷��̾� ĳġ �κ��� ��ħ
            this.Check_PositionDistance_Func();
        }
        else
        {
            UserSystem_Manager.Instance.playInfo.Set_ScorePlayInfo_Func(-(this._myData.ItemScore));
            //ȿ����
            SoundSystem_Manager.Instance.PlaySfx_Func(SfxType.��������ȿ����);

            bool a_Random = Random.Range(0, 2) == 0 ? true : false;

            if(a_Random == true)
            {
                SoundSystem_Manager.Instance.PlaySfx_Func(SfxType.�Ҹ�1);
            }
            else
            {
                SoundSystem_Manager.Instance.PlaySfx_Func(SfxType.�Ҹ�2);
            }
        }

        ItemSystem_Manager.Instance.Set_CountDown_Func();
    }

    private void Check_PositionDistance_Func()
    {
        float a_Distance = 0.0f;
        a_Distance = Vector2.Distance(this.transform.position, PlayerSystem_Manager.Instance.playerMoveScript.Get_ChtchPointTr_Func().position);
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
