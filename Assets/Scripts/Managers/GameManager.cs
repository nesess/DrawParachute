using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private UIManager _uIManager;

    [SerializeField]
    private Rigidbody _playerRigid;

    public bool isPlayerDead = false;
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void PlayerDead()
    {
        isPlayerDead = true;
        _uIManager.SwitchGameOverScene();
    }

}
