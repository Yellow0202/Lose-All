using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold.FrameWork;
using Sirenix.OdinInspector;
using Cargold;

public class SoundChild_Script : SoundSystem_Manager
{
    public static SoundChild_Script Instance;

    [SerializeField, LabelText("ù��° BGM")] private AudioSource _bgnFirstSource;

    [SerializeField, LabelText("SFX �ҽ���")] private List<AudioSource> _sfxSourceList;

    public override void Init_Func(int _layer)
    {
        base.Init_Func(_layer);

        if(_layer == 0)
        {
            Instance = this;
        }
    }

    public void Stop_Bgm_Func()
    {
        this._bgnFirstSource.Stop();
        this._bgnFirstSource.clip = null;
    }

    public void Play_SFXSound_Func(SfxType a_Sfxtype)
    {
        if (this.sfxDataDic.TryGetValue(a_Sfxtype, out Sound_C.SfxData a_Value))
        {
            for (int i = 0; i < this._sfxSourceList.Count; i++)
            {
                if (this._sfxSourceList[i].isPlaying == false)
                {
                    this._sfxSourceList[i].clip = a_Value.clip;
                    this._sfxSourceList[i].volume = a_Value.volume;
                    this._sfxSourceList[i].Play();
                    return;
                }
            }
        }
    }

    public void Start_InGameBgmSound_Func(BgmType a_bgmType)
    {   //�ܼ� �ΰ��� ���带 ���� �Լ�.
        //������ ���� ����... �̷� �� �ۿ� ������.
        if (this.bgmDataDic.TryGetValue(a_bgmType, out Sound_C.BgmData a_Value))
        {
            this._bgnFirstSource.Stop();
            this._bgnFirstSource.clip = a_Value.clip;
            this._bgnFirstSource.volume = a_Value.volume;
            this._bgnFirstSource.Play();

            if(a_bgmType == BgmType.�ΰ���BGMintro)
                StartCoroutine(this.Start_InGameBgmSound_Cor());
        }
    }

    private IEnumerator Start_InGameBgmSound_Cor()
    {
        while (this._bgnFirstSource.isPlaying == true)
        {
            yield return null;
        }

        this.PlayBgm_Func(BgmType.�ΰ���BGMLoop);
        StopCoroutine(this.Start_InGameBgmSound_Cor());
    }

    public void Click_UIBtnSound_Func()
    {
        this.Play_SFXSound_Func(SfxType.��ġ��);
    }
}
