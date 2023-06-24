using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private bool _enableMovement = false;

    public static Action Catch;
    public static Action Fail;

 
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Glove"))
        {
            _enableMovement    = false;
            transform.position = other.transform.position;
            Catch?.Invoke();
        }
        else if (other.CompareTag("Wall"))
        {
            _enableMovement = false;
            Fail?.Invoke();
        }
    }
    
    public void Move() 
    {
        _enableMovement = true;
    }

    public void Stop() 
    {
        _enableMovement = false;
    }

    private void FixedUpdate()
    {   
        if(_enableMovement)
        {
            transform.position  += Vector3.down * (Time.deltaTime * speed);
        }
        
    }
}
