using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{

    #region Inspector Variables

    [SerializeField] private bool hasAnimations;
    [SerializeField] private float speed;
    [SerializeField] private Transform dest1;
    [SerializeField] private Transform dest2;

    #endregion

    #region Private Variables

    private FurnitureManager fm;
    private AnimationController animationController;
    private Rigidbody2D rb2D;
    private Collider2D col2D;
    private Transform destination;

    private bool frozen;

    #endregion

    #region Public Variables

    [NonSerialized] public Direction facingDirection;

    #endregion

    void Start()
    {
        if (hasAnimations) animationController = gameObject.AddComponent<AnimationController>();
        rb2D = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();
        fm = FurnitureManager.instance;
        destination = dest1;
    }

    private void FixedUpdate()
    {
        if (frozen) return;
        CheckDestination();
        Vector2 velocity = (Vector2) this.destination.position - rb2D.position;
        Vector3 newPos = Vector2.MoveTowards(rb2D.position, destination.position, speed * Time.deltaTime);
        facingDirection = DirectionUtils.VectorDirection(velocity);
        SetZPos(newPos);
    }

    private void CheckDestination()
    {
        Vector2 pos = rb2D.position;
        if (pos != (Vector2) destination.position) return;
        if (destination == dest1) destination = dest2;
        else if (destination == dest2) destination = dest1;
    }

    void SetZPos(Vector3 currentPos)
    {
        float z = currentPos.y;
        Collider2D collider = col2D;
        float offset = collider.offset.y;
        z += offset;
        z -= collider.bounds.size.y / 2;
        z = fm.normalize(z);
        rb2D.MovePosition(new Vector3(currentPos.x, currentPos.y, z));
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().frozen = true;
            StartCoroutine(RestartGuardScene());
        }
    }

    IEnumerator RestartGuardScene()
    {
        frozen = true;
        yield return AudioManager.instance.WaitForPlay("Caught Alarm");
        GameManager.instance.LoadGuardScene();
    }
}
