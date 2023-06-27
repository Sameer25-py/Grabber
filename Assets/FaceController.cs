using DefaultNamespace;
using UnityEngine;

public class FaceController : MonoBehaviour
{
    [SerializeField] private Sprite def, happy, sad;

    private SpriteRenderer _renderer;

    private void OnEnable()
    {
        _renderer             =  GetComponent<SpriteRenderer>();
        GameManager.StartTurn += OnStartTurnCalled;
        Ball.Catch            += OnBallCatch;
        Ball.Fail             += OnBallFail;
    }

    private void OnBallFail()
    {
        _renderer.sprite = sad;
    }

    private void OnBallCatch()
    {
        _renderer.sprite = happy;
    }

    private void OnStartTurnCalled()
    {
        _renderer.sprite = def;
    }

    private void Start()
    {
        _renderer.sprite = def;
    }

    private void OnDisable()
    {
        GameManager.StartTurn -= OnStartTurnCalled;
        Ball.Catch            -= OnBallCatch;
        Ball.Fail             -= OnBallFail;
    }
}