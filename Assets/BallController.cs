using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    [SerializeField] private Ball ball;

    [SerializeField] private float   ballIniitalVerticalOffset;
    [SerializeField] private Vector2 minMaxHorizontalBallOffset = new Vector2(-1.7f, 1.7f);

    private void OnEnable()
    {
        Ball.Catch += OnBallCatch;
    }

    private void OnBallCatch()
    {
        ball.transform.position = Vector3.up * ballIniitalVerticalOffset;
    }

    private void OnDisable()
    {
        Ball.Catch -= OnBallCatch;
    }

    public void ThrowBall()
    {
        float randomXOffset = Random.Range(minMaxHorizontalBallOffset.x, minMaxHorizontalBallOffset.y);
        ball.transform.position = new Vector3(randomXOffset, ballIniitalVerticalOffset, 0f);
        ball.Move();
    }

    private void Start()
    {
        ThrowBall();
    }
}