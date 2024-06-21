using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Game : MonoBehaviour
{
    public static Game Instance;

    [SerializeField, FoldoutGroup("플레이어"), LabelText("플레이어 이동속도")] private float _player_MoveSpeed; public float player_MoveSpeed => this._player_MoveSpeed;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
