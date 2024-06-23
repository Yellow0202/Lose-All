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

    [SerializeField, LabelText("������� ��ƼŬ")] private ParticleSystem _horrorFire;
    [SerializeField, LabelText("�׾Ƹ� ��ƼŬ")] private ParticleSystem _miusItem;

    [SerializeField, LabelText("���� ������")] private Vector3 _itemScale;

    [LabelText("���� ���� ������")] private Item_InfoData _myData;

    [LabelText("��������� �پ� �ִ���")] private bool is_Horror;

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
        this._itemSpriteRenderer.sprite = this._myData.Icon;
        //this._itemSpriteRenderer.sprite = ItemSystem_Manager.Instance.Get_ItemIntKeyToSprite_Func(this._myData.IntKey);

        //������ ������ٵ� �� �߰�
        this._itemRid.mass = a_ItemData.Mass;
        this._itemRid.gravityScale = DataBase_Manager.Instance.GetTable_Define.item_Falling_Speed;

        this.is_Horror = Random.Range(0.0f, 100.0f) <= DataBase_Manager.Instance.GetTable_Define.item_Horror_Vlaue ? true : false;

        if (is_Up == true)
        {
            //������ ��¦ ���� �ö󰡾���.
            Vector2 a_StartMoveVec = Vector2.zero;
            a_StartMoveVec.x = Random.Range(-3, 4);
            a_StartMoveVec.y = 5;

            this._itemRid.velocity = a_StartMoveVec;
        }

        if(this._myData.IntKey == 10022)
        {
            this._miusItem.gameObject.SetActive(true);
            this._miusItem.Play();

            this.is_Horror = false;

            this._horrorFire.gameObject.SetActive(false);
            this._horrorFire.Stop();
        }
        else
        {
            this._miusItem.gameObject.SetActive(false);
            this._miusItem.Stop();

            if (this.is_Horror == true)
            {
                this._horrorFire.gameObject.SetActive(true);
                this._horrorFire.Play();
            }
            else
            {
                this._horrorFire.gameObject.SetActive(false);
                this._horrorFire.Stop();
            }
        }
    }

    private void Delete_Func(bool is_Player = false)
    {
        PoolingSystem_Manager.Instance.Despawn_Func(PoolingKey.ItempPoolingKey, this);
        this.gameObject.SetActive(false);

        if(is_Player == true)
        {
            if (InGameUISystem_Manager.s_GameState == GameState.Playing)
                InGameUiAnim_Script.Instance.Call_ItemAnim_Func(this._myData.IntKey);

            UserSystem_Manager.Instance.playInfo.Set_ScorePlayInfo_Func(this._myData.ItemScore);
            EnemySystem_Manager.Instance.Check_CurGetScoreToItemCoolTimeDown_Func(this._myData.ItemScore);
            EnemySystem_Manager.Instance.Check_EnemySpawnCondition_Func();

            ItemSystem_Manager.Instance.Call_ItemGameObject_Func(this._myData.IntKey);

            //true�� ���Դٴ� �� ĳġ�� �������� ����ٴ� ��.
            //�׷� �����۰� �÷��̾� ĳġ �κ��� ��ħ
            this.Check_PositionDistance_Func();
        }
        else
        {
            if (InGameUISystem_Manager.s_GameState == GameState.Playing)
            {
                UserSystem_Manager.Instance.playInfo.Set_SmashedScorePlayInfo_Func(this._myData.ItemScore);
            }

            if (this._myData.IntKey != 10022)
            {

                if (InGameUISystem_Manager.s_GameState == GameState.Playing)
                {
                    UserSystem_Manager.Instance.playInfo.Set_SmashedItemCountPlayInfo_Func();
                }

                //ȿ����
                SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.��������ȿ����);

                bool a_Random = Random.Range(0, 2) == 0 ? true : false;

                if (a_Random == true)
                {
                    SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.�Ҹ�1);
                }
                else
                {
                    SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.�Ҹ�2);
                }

                //�÷��̾� ĳġ ���� UI ���
                PlayerSystem_Manager.Instance.Call_ChtchBalloon_Func(ChtchBalloon.miss);
            }

            if(this.is_Horror == true)
            {
                //�ͽ� ������ �ö�;� ��.
                InGameUiAnim_Script.Instance.Call_GhostAnimOn_Func();
                SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.����);
            }

        }

        InGameUISystem_Manager.Instance.Score_Update_Func();
        InGameUISystem_Manager.Instance.SmashedScore_Update_Func();
        ItemSystem_Manager.Instance.Set_CountDown_Func();
    }

    private void Check_PositionDistance_Func()
    {
        float a_Distance = 0.0f;
        a_Distance = Vector2.Distance(this.transform.position, InGameUISystem_Manager.Instance.playerMove_Script.Get_ChtchPointTr_Func().position);

        if(a_Distance <= DataBase_Manager.Instance.GetTable_Define.chtch_PerpectPersent)
        {
            //����Ʈ UI ���
            SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.����Ʈ);
            PlayerSystem_Manager.Instance.Call_ChtchBalloon_Func(ChtchBalloon.perfect);
        }
        else
        {
            //�� UI ���
            SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.��);
            PlayerSystem_Manager.Instance.Call_ChtchBalloon_Func(ChtchBalloon.good);
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
