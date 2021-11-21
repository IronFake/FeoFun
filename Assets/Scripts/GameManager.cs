using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private TowerMover towerMover;
    
    public void StartGame()
    {
        towerMover.enabled = true;
    }

    public void FinishGame()
    {
        towerMover.enabled = false;
        TowerController.Instance.PrepareToStart();
    }
}
