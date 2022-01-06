using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    #region Inspector Variables

    [SerializeField]
    private float xSpeed;
    [SerializeField]
    private float ySpeed;

    [SerializeField] private float gravity;
    [SerializeField] private float jumpVel;
    
    [SerializeField] private bool hasAnimations;
    
    #endregion
    
    #region Cached Components
    
    //private Animator animator;
    private AnimationController animationController;
    private Rigidbody2D rb2D;
    private Collider2D col2D;
    private Vector3 scale;
    private FurnitureManager fm;
    private CinemachineVirtualCamera cVCam;

    #endregion

    #region Velocity Variables

    //[SerializeField] private Vector2 velocity;
    private Vector2 rawVelocity;
    private Vector2 lastVelocity;

    #endregion
    
    #region Private variables

    [SerializeField] private bool isJumping = false;
    [SerializeField] private Vector3 startJumpPos;
    [SerializeField] private float jumpYDelta;
    private bool space;
    [SerializeField] private float yJumpVel;
    [NonSerialized]
    public static PlayerMovement instance;
    
    #endregion

    #region Public Variables

    [NonSerialized] public Direction facingDirection;
    [NonSerialized] public bool frozen;
    
    #endregion

    #region Instantiation Methods

    void Start()
    {
        instance = this;
        if (hasAnimations) animationController = gameObject.AddComponent<AnimationController>();
        rb2D = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();
        scale = this.transform.localScale;
        fm = FurnitureManager.instance;
        SetZPos(rb2D.position);
        SpawnCharacter();
        SceneManager.sceneLoaded += (arg0, mode) => frozen = false;
    }

    public void SpawnCharacter()
    {
        cVCam = FindObjectOfType<CinemachineVirtualCamera>();
        if (cVCam != null)
        {
            Debug.Log("Setting cvcam");
            cVCam.Follow = FindObjectOfType<PlayerMovement>().gameObject.transform;
        }
    }

    #endregion
    
    #region Update Functions

    void Update()
    {
        float xVel = Input.GetAxis("Horizontal");
        float yVel = Input.GetAxis("Vertical");
        rawVelocity = new Vector2(xVel, yVel);
        space = Input.GetKeyDown("space"); 
        JumpHandler();
    }

    private void FixedUpdate()
    {
        if (frozen) return;
        Vector2 velocity = new Vector2(rawVelocity.x * xSpeed * Time.deltaTime, rawVelocity.y * ySpeed * Time.deltaTime);
        facingDirection = DirectionUtils.VectorDirection(lastVelocity);
        //rb2D.MovePosition(rb2D.position + velocity);
        SetZPos(rb2D.position + velocity);
    }

    private void LateUpdate()
    {
        if (animationController) SetAnimatorVars(rawVelocity);
        if (rawVelocity.sqrMagnitude > 0.001f)
        {
            lastVelocity = rawVelocity;
        }
    }
    
    #endregion

    
    #region Jump Methods

    void JumpHandler()
    {
        if (space && !isJumping)
        {
            Debug.Log("space");
            StartJump();
        } 
        else if (isJumping)
        {
            jumpYDelta += rawVelocity.y * ySpeed * Time.deltaTime;
            float currDelta = transform.position.y - startJumpPos.y;
            // yJumpVel is the original jump velocity + accumulating gravity
            // necessary because rawVelocity resets every frame to x/y inputs
            yJumpVel -= (gravity * ySpeed * Time.deltaTime);
            //checks to see if you've gone down farther than you should've
            if (currDelta <= jumpYDelta + 0.000001f && yJumpVel < 0)
            {
                EndJump();
            }
            else if (yJumpVel < 0)
            {
                float maxYIncrease = Mathf.Max(yJumpVel, -1 * currDelta);
                rawVelocity += Vector2.up * maxYIncrease;
            } 
            else 
            {
                rawVelocity += Vector2.up * yJumpVel;
            }
        }
    }

    void StartJump()
    {
        rawVelocity += jumpVel * Vector2.up;
        yJumpVel = jumpVel;
        startJumpPos = this.transform.position;
        jumpYDelta = 0;
        //this.transform.position += 0.001f * Vector3.up;
        isJumping = true;
    }

    void EndJump()
    {
        isJumping = false;
    }

    #endregion
    
    #region Animator Functions
    
    void SetAnimatorVars(Vector3 velocity)
    {
        
        animationController.SetDirection(facingDirection);
        animationController.SetMovement(velocity);
        animationController.UpdateAnimation();
        
    }

    #endregion

    #region Utility Methods
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
    
    float SignOrZero(float x)
    {
        if (Math.Abs(x) <= 0.001f)
        {
            return 0;
        } 
        else if (x > 0)
        {
            return 1;
        }
        return -1;
    }

    //Gets a unit vector in direction currently facing
    public Vector2 UnitVectorFacing()
    {
        Bounds bounds = col2D.bounds;
        bounds.Expand(0.2f);
        return DirectionUtils.UnitVector(this.facingDirection) * bounds.size;
    }

    public Vector2 feetCenter()
    {
        return col2D.bounds.center;
    }

    public void Unfreeze()
    {
        frozen = false;
    }

    #endregion
    
}
