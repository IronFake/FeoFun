using System;
using FeoFun.Inputs;
using UnityEngine;

namespace FeoFun.Core
{
    public class GameManager : Singleton<GameManager>
    {
        public static event Action ONGameStart;
        public static event Action ONGameRestart;

        [SerializeField] private GameObject startingText;

        private bool _isStarted;
        private InputManager _inputManager;

        private void Start()
        {
            _inputManager = InputManager.Instance;
        }

        private void StartGame()
        {
            ONGameStart?.Invoke();
            startingText.SetActive(false);
            _isStarted = true;
        }

        public void RestartGame()
        {
            ONGameRestart?.Invoke();
            startingText.SetActive(true);
            _isStarted = false;
        }

        private void Update()
        {
            if (_isStarted)
                return;

            if (_inputManager.AnyKey)
            {
                StartGame();
            }
        }
    }
}
