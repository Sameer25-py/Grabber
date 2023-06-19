using System;
using UnityEngine;

public class GloveController : MonoBehaviour
{
    [SerializeField] private Vector2 minMaxHorizontalPosition = new(-1.7f, 1.7f);
    [SerializeField] private float   speed                    = 2f;
    [SerializeField] private Vector2 initialPosition          = new Vector2(-1.7f, 0f);
    private                  Camera  _mainCamera;
    private                  Vector2 _mousePosition;

    public bool EnableInput = false;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        _mousePosition   = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _mousePosition.x = Mathf.Clamp(_mousePosition.x, minMaxHorizontalPosition.x, minMaxHorizontalPosition.y);
        _mousePosition.y = 0f;
        EnableInput      = true;
    }

    private void FixedUpdate()
    {
        if (!EnableInput) return;
        transform.position = Vector2.Lerp(transform.position, _mousePosition, Time.deltaTime * speed);
        if (Vector2.Distance(transform.position, _mousePosition) < 0.01f)
        {
            //moveBack
            transform.position = Vector2.Lerp(_mousePosition, initialPosition, Time.deltaTime * speed);
            _mousePosition     = transform.position;
        }
    }
}