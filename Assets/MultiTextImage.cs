using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class MultiTextImage : MonoBehaviour
    {
        [SerializeField] private Sprite eng, rus;

        private Image _image;

        private void OnEnable()
        {
            _image                     =  GetComponent<Image>();
            OnChangeLanguageCalled(GameManager.Language);
            GameManager.ChangeLanguage += OnChangeLanguageCalled;
        }

        private void OnChangeLanguageCalled(string obj)
        {
            _image.sprite = obj == "rus" ? rus : eng;
        }
    }
}