using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold.FrameWork;
using Sirenix.OdinInspector;
using Cargold;

public class SoundChild_Script : SoundSystem_Manager
{
    public static SoundChild_Script Instance;

    [SerializeField, LabelText("첫번째 BGM")] private AudioSource _bgnFirstSource;

    [SerializeField, LabelText("SFX 소스들")] private List<AudioSource> _sfxSourceList;

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
    {   //단순 인게임 사운드를 위한 함수.
        //실패한 자의 흔적... 이럴 수 밖에 없었다.
        if (this.bgmDataDic.TryGetValue(a_bgmType, out Sound_C.BgmData a_Value))
        {
            this._bgnFirstSource.Stop();
            this._bgnFirstSource.clip = a_Value.clip;
            this._bgnFirstSource.volume = a_Value.volume;
            this._bgnFirstSource.Play();

            if(a_bgmType == BgmType.인게임BGMintro)
                StartCoroutine(this.Start_InGameBgmSound_Cor());
        }
    }

    private IEnumerator Start_InGameBgmSound_Cor()
    {
        while (this._bgnFirstSource.isPlaying == true)
        {
            yield return null;
        }

        this.PlayBgm_Func(BgmType.인게임BGMLoop);
        StopCoroutine(this.Start_InGameBgmSound_Cor());
    }

    public void Click_UIBtnSound_Func()
    {
        this.Play_SFXSound_Func(SfxType.터치음);
    }
}
