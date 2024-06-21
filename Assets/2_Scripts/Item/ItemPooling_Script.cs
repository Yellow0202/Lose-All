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
    [SerializeField, LabelText("아이템 아이콘")] private Image _itemIcon;


    [LabelText("전달 받은 데이터")] private Item_InfoData _myData;

    public void InitializedByPoolingSystem()
    {
        this.InIt_Func();
    }

    private void InIt_Func()
    {

    }

    public void Setting_Func(Item_InfoData a_ItemData)
    {
        this._myData = a_ItemData;
    }

    private void Delete_Func()
    {
        PoolingSystem_Manager.Instance.Despawn_Func(PoolingKey.ItempPoolingKey, this);
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            this.Delete_Func();
        }
    }
}
