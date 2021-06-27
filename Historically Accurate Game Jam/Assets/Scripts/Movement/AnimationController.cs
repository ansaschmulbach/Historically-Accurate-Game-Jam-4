using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    private bool isMoving;
    private Direction currentDirection;

    void Awake()
    {
        this.animator = GetComponent<Animator>();
    }

    public void UpdateAnimation()
    {
        string animation = currentDirection.ToString().ToLowerInvariant();
        animation += isMoving ? " walk" : " idle";
        animator.Play(animation);
    }

    public void SetDirection(Direction direction)
    {
        currentDirection = direction;
    }

    public void SetMovement(Vector3 velocity)
    {
        isMoving = velocity.sqrMagnitude > 0.0001f;
    }
    
}
