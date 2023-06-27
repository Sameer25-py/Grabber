using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        public TMP_Text   Score, UiScore;
        public Ball       Ball;
        public GameObject InGameUI, PauseMenu, EndMenu;

        public static Action         StartTurn;
        public static Action         EndTurn;
        public static Action<string> ChangeLanguage;

        public static string Language = "rus";

        private int _score = 0;

        private void OnEnable()
        {
            Ball.Catch += OnBallCatch;
            Ball.Fail  += OnBallFail;
        }

        private void OnBallFail()
        {
            Invoke(nameof(CallEndTurn), 0.2f);
            Invoke(nameof(EndGame), 0.8f);
        }

        private void OnBallCatch()
        {
            _score       += 1;
            Score.text   =  _score.ToString();
            UiScore.text =  _score.ToString();
            Invoke(nameof(CallEndTurn), 0.2f);
            Invoke(nameof(StartNextTurn), 0.5f);
        }

        private void CallEndTurn()
        {
            EndTurn?.Invoke();
        }

        public void EndGame()
        {
            EndMenu.SetActive(true);
            InGameUI.SetActive(false);
        }

        private void StartNextTurn()
        {
            StartTurn?.Invoke();
        }

        private void StartGame()
        {
            _score       = 0;
            Score.text   = _score.ToString();
            UiScore.text = _score.ToString();
            StartNextTurn();
        }

        public void PauseGame()
        {
            Ball.Stop();
            Ball.gameObject.SetActive(false);
            InGameUI.SetActive(false);
            PauseMenu.SetActive(true);
        }

        public void ReturnToGame()
        {
            Ball.gameObject.SetActive(true);
            Ball.Move();
            InGameUI.SetActive(true);
            PauseMenu.SetActive(false);
        }

        public void RestartGame()
        {
            EndMenu.SetActive(false);
            InGameUI.SetActive(true);
            StartNextTurn();
        }

        public void MainMenu() { }

        private void Start()
        {
            StartGame();
        }
    }
}