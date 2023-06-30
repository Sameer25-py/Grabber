using System;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        public TMP_Text   Score, UiScore;
        public Ball       Ball;
        public GameObject InGameUI, PauseMenu, EndMenu, Gameplay, MainMenu, Settings;
        public GameObject FirstTimeUI;

        public static Action         StartTurn;
        public static Action         EndTurn;
        public static Action<string> ChangeLanguage;


        public AudioSource AudioSource;

        public static string Language = "eng";

        private bool _enableVibration = true;

        private int _score = 0;

        private void OnEnable()
        {
            Ball.Catch += OnBallCatch;
        }

        private void OnBallFail()
        {
            Invoke(nameof(CallEndTurn), 0.2f);
            Invoke(nameof(EndGame), 0.8f);
        }

        private void OnBallCatch()
        {
            if (_enableVibration)
            {
                Handheld.Vibrate();
            }

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
            Ball.Fail    += OnBallFail;
            _score       =  0;
            Score.text   =  _score.ToString();
            UiScore.text =  _score.ToString();
            InGameUI.SetActive(true);
            Ball.gameObject.SetActive(true);
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
            StartGame();
        }

        public void Play()
        {
            MainMenu.SetActive(false);
            Gameplay.SetActive(true);
            FirstTimeUI.SetActive(true);
            StartGame();
        }

        public void ShowMainMenu()
        {
            Ball.Fail -= OnBallFail;
            MainMenu.SetActive(true);
            Gameplay.SetActive(false);
            Settings.SetActive(false);
            EndMenu.SetActive(false);
            PauseMenu.SetActive(false);
        }

        public void ReturnToMenu()
        {
            Settings.SetActive(false);
            MainMenu.SetActive(true);
        }

        public void ShowSettings()
        {
            Settings.SetActive(true);
            MainMenu.SetActive(false);
        }

        public void ToggleVibration(bool state)
        {
            _enableVibration = state;
        }

        public void ToggleLanguage(bool state)
        {
            Language = state ? "eng" : "rus";
            ChangeLanguage?.Invoke(Language);
        }

        public void ToggleSound(bool state)
        {
            AudioSource.mute = !state;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                FirstTimeUI.SetActive(false);
            }
        }
    }
}