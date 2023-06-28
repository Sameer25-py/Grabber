using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class SettingsButton : MonoBehaviour
    {
        [SerializeField] private Sprite active, disabled;

        private Image  _image;
        private Button _button;
        private bool   _state = true;
        
        public UnityEvent<bool> ChangeStateCallback;

        private void OnEnable()
        {
            _button = GetComponent<Button>();
            _image  = GetComponent<Image>();

            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            _state        = !_state;
            _image.sprite = _state ? active : disabled;
            ChangeStateCallback?.Invoke(_state);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }
    }
}