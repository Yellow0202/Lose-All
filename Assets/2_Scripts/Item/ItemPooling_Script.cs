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

    [SerializeField, LabelText("도깨비불 파티클")] private ParticleSystem _horrorFire;
    [SerializeField, LabelText("항아리 파티클")] private ParticleSystem _miusItem;

    [SerializeField, LabelText("변경 스케일")] private Vector3 _itemScale;

    [LabelText("전달 받은 데이터")] private Item_InfoData _myData;

    [LabelText("도깨비불이 붙어 있는지")] private bool is_Horror;

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

        //아이템 이미지 변경
        this._itemSpriteRenderer.sprite = this._myData.Icon;
        //this._itemSpriteRenderer.sprite = ItemSystem_Manager.Instance.Get_ItemIntKeyToSprite_Func(this._myData.IntKey);

        //지정된 리지드바디 값 추가
        this._itemRid.mass = a_ItemData.Mass;
        this._itemRid.gravityScale = DataBase_Manager.Instance.GetTable_Define.item_Falling_Speed;

        this.is_Horror = Random.Range(0.0f, 100.0f) <= DataBase_Manager.Instance.GetTable_Define.item_Horror_Vlaue ? true : false;

        if (is_Up == true)
        {
            //스폰시 살짝 위로 올라가야함.
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

            //true가 나왔다는 건 캐치로 아이템을 얻었다는 것.
            //그럼 아이템과 플레이어 캐치 부분의 겹침
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

                //효과음
                SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.떨어지는효과음);

                bool a_Random = Random.Range(0, 2) == 0 ? true : false;

                if (a_Random == true)
                {
                    SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.소름1);
                }
                else
                {
                    SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.소름2);
                }

                //플레이어 캐치 실패 UI 출력
                PlayerSystem_Manager.Instance.Call_ChtchBalloon_Func(ChtchBalloon.miss);
            }

            if(this.is_Horror == true)
            {
                //귀신 사진이 올라와야 함.
                InGameUiAnim_Script.Instance.Call_GhostAnimOn_Func();
                SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.유령);
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
            //퍼펙트 UI 출력
            SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.퍼펙트);
            PlayerSystem_Manager.Instance.Call_ChtchBalloon_Func(ChtchBalloon.perfect);
        }
        else
        {
            //굿 UI 출력
            SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.굿);
            PlayerSystem_Manager.Instance.Call_ChtchBalloon_Func(ChtchBalloon.good);
        }

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
            this.Delete_Func(true);
        }
    }
}
