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

    [SerializeField, LabelText("���� ���� ����"), ReadOnly] private int _curItemSpawnCount;
    [SerializeField, LabelText("���� ���� �ð�"), ReadOnly] private float _curItemSpawnTime;

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

        while (true)
        {
            if(this._curItemSpawnTime < DataBase_Manager.Instance.GetTable_Define.item_Spawn_CoolTime)
                this._curItemSpawnTime += Time.deltaTime;

            if(DataBase_Manager.Instance.GetTable_Define.item_Spawn_CoolTime <= this._curItemSpawnTime)
            {
                this._curItemSpawnTime = 0.0f;

                if (this._curItemSpawnCount < DataBase_Manager.Instance.GetTable_Define.item_Spawn_Count)
                {
                    //Ȯ�� ��� ����
                    float a_Persent = Random.Range(0, 100.0f);
                    Item_InfoData a_ItemData = this.Get_ItemRareToItemData_Func(a_Persent);

                    //��� ���� ��ġ ����
                    Vector2 a_SpawnVec = Vector2.zero;

                    a_SpawnVec.x = Random.Range(-8.0f, 8.0f);
                    a_SpawnVec.y = 4.5f;

                    //������ ��ȯ
                    ItemPooling_Script _ItemPrefab = PoolingSystem_Manager.Instance.Spawn_Func<ItemPooling_Script>(PoolingKey.ItempPoolingKey);
                    _ItemPrefab.Setting_Func(a_ItemData, a_SpawnVec);

                    this._curItemSpawnCount++;
                }
            }

            yield return null;
        }
    }

    private Item_InfoData Get_ItemRareToItemData_Func(float a_Persent)
    {   //���� ���� ���� ���� ����� ����. ������ ����� �ָ� �Ѱ��ش�.
        Rare a_ItemRare = DataBase_Manager.Instance.GetItem_RarePersent.Get_ItemRareToPersentValueDataDic_Func(a_Persent);

        return DataBase_Manager.Instance.GetItem_Info.Get_ItemRareToItemInfoDataListDataDic_Func(a_ItemRare);
    }

    public void Set_CountDown_Func()
    {
        this._curItemSpawnCount--;

        if (this._curItemSpawnCount < 0)
            this._curItemSpawnCount = 0;
    }
}
