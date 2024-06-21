using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;
using Cargold.FrameWork;

public class ItemSystem_Manager : SerializedMonoBehaviour, Cargold.FrameWork.GameSystem_Manager.IInitializer
{
    public static ItemSystem_Manager Instance;
    private CoroutineData _itemSpawnCorData;

    [SerializeField, LabelText("현재 생성 갯수"), ReadOnly] private int _curItemSpawnCount;
    [SerializeField, LabelText("현재 생성 시간"), ReadOnly] private float _curItemSpawnTime;

    public void Init_Func(int _layer)
    {
        if (_layer == 0)
        {
            Instance = this;
        }
        else if (_layer == 1)
        {

        }
        else if (_layer == 2)
        {
            this.Start_ItemSpawn_Func();
        }
    }

    public void Start_ItemSpawn_Func()
    {
        this.ItemSpawn_Func();
    }

    private void ItemSpawn_Func()
    {
        this._curItemSpawnTime = DataBase_Manager.Instance.GetTable_Define.item_Spawn_CoolTime;
        this._itemSpawnCorData.StartCoroutine_Func(ItemSpawn_Cor(), CoroutineStartType.StartWhenStop);
    }

    private IEnumerator ItemSpawn_Cor()
    {
        while(true)
        {
            if(this._curItemSpawnTime < DataBase_Manager.Instance.GetTable_Define.item_Spawn_CoolTime)
                this._curItemSpawnTime += Time.deltaTime;

            if(DataBase_Manager.Instance.GetTable_Define.item_Spawn_CoolTime <= this._curItemSpawnTime)
            {
                this._curItemSpawnTime = 0.0f;

                //확률 대상 지정
                float a_Persent = Random.Range(0, 100.0f);
                Item_InfoData a_ItemData = this.Get_ItemRareToItemData_Func(a_Persent);

                Debug.Log("a_ItemData Name : " + a_ItemData.Name);

                //아이템 소환
                ItemPooling_Script _ItemPrefab = PoolingSystem_Manager.Instance.Spawn_Func<ItemPooling_Script>(PoolingKey.ItempPoolingKey);
                _ItemPrefab.Setting_Func(null);
            }

            yield return null;
        }
    }

    private Item_InfoData Get_ItemRareToItemData_Func(float a_Persent)
    {   //전달 받은 값을 토대로 레어도를 지정. 지정된 레어도로 애를 넘겨준다.
        Rare a_ItemRare = DataBase_Manager.Instance.GetItem_RarePersent.Get_ItemRareToPersentValueDataDic_Func(a_Persent);

        return DataBase_Manager.Instance.GetItem_Info.Get_ItemRareToItemInfoDataListDataDic_Func(a_ItemRare);
    }

    private void Set_CountDown_Func()
    {
        this._curItemSpawnCount--;

        if (this._curItemSpawnCount < 0)
            this._curItemSpawnCount = 0;
    }
}
