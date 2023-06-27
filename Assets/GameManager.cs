using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        public        TMP_Text Score;
        public static Action   StartTurn;
        public static Action   EndTurn;

        private int _score = 0;

        private void OnEnable()
        {
            Ball.Catch += OnBallCatch;
            Ball.Fail  += OnBallFail;
        }

        private void OnBallFail()
        {
            Invoke(nameof(CallEndTurn), 0.2f);
            Invoke(nameof(StartGame), 0.8f);
        }

        private void OnBallCatch()
        {
            _score     += 1;
            Score.text =  _score.ToString();
            Invoke(nameof(CallEndTurn), 0.2f);
            Invoke(nameof(StartNextTurn), 0.5f);
        }

        private void CallEndTurn()
        {
            EndTurn?.Invoke();
        }

        private void EndGame() { }

        private void StartNextTurn()
        {
            StartTurn?.Invoke();
        }

        private void StartGame()
        {
            _score = 0;
            Score.text = _score.ToString();
            StartNextTurn();
        }

        private void Start()
        {
            StartGame();
        }
    }
}