using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;

public class GloveController : MonoBehaviour
{
    [SerializeField] private GameObject ballInGlove;
    [SerializeField] private Vector2    minMaxHorizontalPosition = new(-1.7f, 1.7f);
    [SerializeField] private float      speed                    = 2f;
    [SerializeField] private Vector2    initialPosition          = new Vector2(-1.7f, 0f);
    private                  Camera     _mainCamera;
    private                  Vector2    _mousePosition;

    public  bool EnableInput = false;
    private bool _startTurn  = false;

    private void OnEnable()
    {
        Ball.Catch            += OnBallCatch;
        GameManager.StartTurn += OnStartTurnCalled;
        GameManager.EndTurn   += OnEndTurnCalled;
    }

    private void OnEndTurnCalled()
    {
        _startTurn = false;
    }

    private void OnStartTurnCalled()
    {
        _startTurn = true;
        ballInGlove.SetActive(false);
    }
    
    private void OnBallCatch()
    {
        ballInGlove.SetActive(true);
    }

    private void OnDisable()
    {
        Ball.Catch            += OnBallCatch;
        GameManager.StartTurn -= OnStartTurnCalled;
        GameManager.EndTurn   -= OnEndTurnCalled;
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!_startTurn) return;
        if (!Input.GetMouseButtonDown(0)) return;
        _mousePosition   = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _mousePosition.x = Mathf.Clamp(_mousePosition.x, minMaxHorizontalPosition.x, minMaxHorizontalPosition.y);
        _mousePosition.y = initialPosition.y;
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