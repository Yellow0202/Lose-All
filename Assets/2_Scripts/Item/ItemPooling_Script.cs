using Cargold.PoolingSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using Cargold;

public class ItemPooling_Script : MonoBehaviour, IPooler
{
    [SerializeField, LabelText("아이템 스프라이트 렌더러")] private SpriteRenderer _itemSpriteRenderer;
    [SerializeField, LabelText("리지드바디")] private Rigidbody2D _itemRid;
    
    [SerializeField, LabelText("판정 포인트(임시)")] private Transform _itemCheckPointTr;

    [SerializeField, LabelText("변경 스케일")] private Vector3 _itemScale;

    [LabelText("전달 받은 데이터")] private Item_InfoData _myData;

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

        //아이템 이미지 변경
        this._itemSpriteRenderer.sprite = this._myData.Icon;

        //지정된 리지드바디 값 추가
        this._itemRid.mass = a_ItemData.Mass;
        this._itemRid.gravityScale = DataBase_Manager.Instance.GetTable_Define.item_Falling_Speed;

        //스폰시 살짝 위로 올라가야함.
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
        if(coll.gameObject.tag == "Guard")
        {
            //벽에 부딪혔을 경우 어떻게할까?
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
            Debug.Log("호출");
            this.Delete_Func(true);
        }
    }
}
