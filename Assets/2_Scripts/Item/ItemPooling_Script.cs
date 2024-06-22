using Cargold.PoolingSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;
using Cargold;

public class ItemPooling_Script : MonoBehaviour, IPooler
{
    [SerializeField, LabelText("������ ������")] private Image _itemIcon;
    [SerializeField, LabelText("������ٵ�")] private Rigidbody2D _itemRid;


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

        //������ ������ٵ� �� �߰�
        this._itemRid.mass = a_ItemData.Mess;
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
            UserSystem_Manager.Instance.playInfo.Set_ScorePlayInfo_Func(this._myData.ItemScore);
            EnemySystem_Manager.Instance.Check_EnemySpawnCondition_Func();
        }
        else
        {
            UserSystem_Manager.Instance.playInfo.Set_ScorePlayInfo_Func(-(this._myData.ItemScore));
        }

        ItemSystem_Manager.Instance.Set_CountDown_Func();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            this.Delete_Func(true);
        }
        else if(coll.gameObject.tag == "Guard")
        {
            //���� �ε����� ��� ����ұ�?
        }
        else if(coll.gameObject.tag == "DownGuard")
        {
            this.Delete_Func();
        }
    }
}
