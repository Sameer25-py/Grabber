using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class IngameScore : MonoBehaviour
    {
        [SerializeField] private GameObject     glove;
        private                  SpriteRenderer _renderer;

        private void OnEnable()
        {
            _renderer             =  GetComponent<SpriteRenderer>();
            Ball.Catch            += OnBallCatch;
            GameManager.StartTurn += OnStartTurn;
        }

        private void OnStartTurn()
        {
            _renderer.enabled = false;
        }

        private void Start()
        {
            _renderer.enabled = false;
        }

        private void OnBallCatch()
        {
            transform.position = new Vector3(glove.transform.position.x, transform.position.y, 0f);
            _renderer.enabled  = true;

        }
        
        private void OnDisable()
        {
            Ball.Catch            -= OnBallCatch;
            GameManager.StartTurn -= OnStartTurn;
        }
    }
}