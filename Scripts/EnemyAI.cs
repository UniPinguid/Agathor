using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming
    }

    private State state;
    private EnemyPathFinding pathFinding;
    private Animator animator;

    private Vector2 previousPosition;
    private float speed;
    private Vector2 direction;
    private Vector2 smoothedDirection;

    [Range(0.01f, 1.0f)]
    public float directionSmoothTime = 0.1f; // The smoothing factor for the direction

    private void Awake()
    {
        state = State.Roaming;
        pathFinding = GetComponent<EnemyPathFinding>();
        animator = GetComponent<Animator>();
        previousPosition = transform.position;
        smoothedDirection = Vector2.zero;

        speed = pathFinding.moveSpeed;
    }

    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    private void Update()
    {
        CalculateMovementSpeedAndDirection();
        SmoothDirection();
        UpdateAnimator();
    }

    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            pathFinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(1f);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void CalculateMovementSpeedAndDirection()
    {
        Vector2 currentPosition = transform.position;
        Vector2 movement = currentPosition - previousPosition;
        // speed = movement.magnitude / Time.deltaTime;
        direction = movement.normalized;
        previousPosition = currentPosition;
    }

    private void SmoothDirection()
    {
        smoothedDirection = Vector2.Lerp(smoothedDirection, direction, directionSmoothTime);
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("speed", speed);
        animator.SetFloat("moveX", smoothedDirection.x);
        animator.SetFloat("moveY", smoothedDirection.y);
    }
}
