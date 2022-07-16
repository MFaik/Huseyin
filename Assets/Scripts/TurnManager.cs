using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TurnManager : MonoBehaviour 
{
    public static TurnManager Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    PlayerController _playerController;

    void Start() {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _playerController.EnableTurn();
    }

    public static void ContinueTurnAfterPlayer(){
        Instance.StartCoroutine(Instance.ContinueTurn());
    }

    IEnumerator ContinueTurn(){
        yield return EnemyManager.NextStep();
        yield return BulletManager.NextStep();
        _playerController.EnableTurn();
    }
}