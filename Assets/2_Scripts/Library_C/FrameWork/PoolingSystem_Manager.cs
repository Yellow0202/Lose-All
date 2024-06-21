using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;
using Cargold.FrameWork;

public class PoolingSystem_Manager : Cargold.FrameWork.PoolingSystem_Manager
{
    public static new PoolingSystem_Manager Instance;

    [SerializeField] private BasePoolingData<ItemPooling_Script> baseItemPoolingData;

    public override void Init_Func(int _layer)
    {
        base.Init_Func(_layer);

        if(_layer == 0)
        {
            Instance = this;

            base.TryGeneratePool_Func<ItemPooling_Script>(PoolingKey.ItempPoolingKey, this.baseItemPoolingData);
        }
    }
}

public partial class PoolingKey
{
    public const string ItempPoolingKey = "ItemPooling";
}
