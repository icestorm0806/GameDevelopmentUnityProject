/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player_movement : MonoBehaviour
{

    private Rigidbody2D rb2D;
    private GameObject player;
    Animator animator;

    public LayerMask groundLayers;

    public float playerSpeed, jumpHight;

    public bool isgrounded;
    private bool facingLeft, facingRight;
    private float moveHorizontal;
    

    private Vector2 _velocity;
    private Transform _transform;
    private Vector3 _localScale;
    private BoxCollider2D _boxCollider;
    private ControllerParameters2D _overrideParameters;
    private float _jumpIn;

    private Vector3
        _activeGlobalPlatformPoint,
        _activeLocalPlatformPoint;

    private Vector3
        _raycastTopLeft,
        _raycastBottomRight,
        _raycastBottomLeft;

    private float
        _verticalDistanceBetweenRays,
        _horizontalDistanceBetweenRays;

    public void Awake()
    {
        {
            HandleCollisions = true;
            State = new ControllerState2D();
            _transform = transform;
            _localScale = transform.localScale;
            _boxCollider = GetComponent<BoxCollider2D>();
            var colliderWidth = _boxCollider.size.x * Mathf.Abs(transform.localScale.x) - (2 * SkinWidth);
            _horizontalDistanceBetweenRays = colliderWidth / (TotalVerticalRays - 1);
        }
    }

    public void AddForce(Vector2 force)
    {
        _velocity = force;
    }

    public void SetForce(Vector2 force)
    {
        _velocity += force;
    }

    public void SetHorizontalForce(float x)
    {
        _velocity.x = x;
    }

    public void SetVerticalForce(float y)
    {
        _velocity.y = y;
    }

    public void Jump()
    {
        // TODO: Moving platform support
        AddForce(new Vector2(0, DefaultParameters.JumpMagnitude));
        _jumpIn = DefaultParameters.JumpFrequency;
    }

    public void LateUpdate()
    {
        _jumpIn -= Time.deltaTime;
        _velocity.y += DefaultParameters.Gravity * Time.deltaTime;
        Move(Velocity * Time.deltaTime);
    }

    private void Move(Vector2 deltaMovement)
    {
        var wasGrounded = State.IsCollidingBelow;
        State.Reset();

        if (HandleCollisions)
        {
            HandlePlatforms();
            CalculateRayOrigins();

            if (deltaMovement.y < 0 && wasGrounded)
                HandleVerticalSlope(ref deltaMovement);

            if (Mathf.Abs(deltaMovement.x) > .001f)
                MovementHorizontally(ref deltaMovement);

            MoveVertically(ref deltaMovement);
        }

        _transform.Translate(deltaMovement, Space.World);

        if (Time.deltaTime > 0)
            _velocity = deltaMovement / Time.deltaTime;

        _velocity.x = Mathf.Min(_velocity.x, DefaultParameters.MaxVelocity.x);
        _velocity.y = Mathf.Min(_velocity.y, Parameters.MaxVelocity.y);

        if (State.IsMovingUpSlope)
            _velocity.y = 0;

        if (StandingOn != null)
        {
            _activeGlobalPlatformPoint = transform.position;
            _activeLocalPlatformPoint = StandingOn.transform.InverseTransformPoint(transform.position);
        }
    }

    private void HandlePlatforms()
    {
        if (StandingOn != null)
        {
            var newGlobalPlatformPoint = StandingOn.transform.TransformPoint(_activeGlobalPlatformPoint);
            var moveDistance = newGlobalPlatformPoint - _activeGlobalPlatformPoint;

            if (moveDistance != Vector3.zero)
                transform.Translate(moveDistance, Space.World);

            PlatformVelocity = (newGlobalPlatformPoint - _activeGlobalPlatformPoint) / Time.deltaTime;
        }
        else
            PlatformVelocity = Vector3.zero;

        StandingOn = null;
    }

    private void CalculateRayOrigins()
    {
        var size = new Vector2(_boxCollider.size.x * Mathf.Abs(_localScale.x), _boxCollider.size.y * Mathf.Abs(_localScale.y)) / 2;
        var center = new Vector2(_boxCollider.center.x * _localScale.x, _boxCollider.center.y * _localScale.y);

        _raycastT0pLeft = _transform.position + new Vector3(center.x - size.x + SkinWoidth, center.y + size.y - SkinWidth);
        _raycastBottomRight = _transform.position + new Vector3(center.x + size.x - SkinWidth, center.y - size.y + SkinWidth);
        _raycastBottomLeft = _transform.position + new Vector3(center.x - size.x + SkinWidth, center.y - size.y + SkinWidth);)
    }

    private void MoveHorizontally(ref Vector2 deltaMovement)
    {
        var isGoingRight = deltaMovement.x > 0;
        var rayDistance = Mathf.Abs(deltaMovement.x) + SkinWidth;
        var rayDirection = isGoingRight ? Vector2.right : -Vector2.right;
        var rayOrigin = isGoingRight ? _raycastBottomRight : _raycastBottomLeft;


    }

    
}




















/**************************************/
/*
void Start()
{
    isgrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.05f, transform.position.y - 0.05f)
        , new Vector2(transform.position.x + 0.005f, transform.position.y - 0.051f), groundLayers);
    facingRight = true;
    rb2D = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
}

void Update()
{
    if (isgrounded == true)
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpHight);
        }

        transform.parent = null;
    }
}

void FixedUpdate()
{
    moveHorizontal = Input.GetAxis("Horizontal");
    if (moveHorizontal < 0)
    {
        TurnLeft();
    }
    if (moveHorizontal > 0)
    {
        TurnRight();
    }
    animator.SetFloat("moveHorizontal", Mathf.Abs(moveHorizontal));
    moveHorizontal = moveHorizontal * Time.deltaTime * playerSpeed;
    transform.Translate(moveHorizontal, 0, 0);
    // Prevents movement beyond those x coordinates
    //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4.85f, 4.85f),
    //transform.position.y, transform.position.z);
}

void OnCollisionEnter2D(Collision2D other)
{
    if ((other.gameObject.CompareTag("ground")) || (other.gameObject.CompareTag("platform")))
    {
        isgrounded = true;
    }

    if (other.gameObject.name.StartsWith("block"))
        rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
}

void OnCollisionExit2D(Collision2D other)
{
    if ((other.gameObject.CompareTag("ground")) || (other.gameObject.CompareTag("platform")))
    {
        isgrounded = false;
    }
}



void OnTriggerStay(Collider other)
{

    if (other.gameObject.tag == "platform")
    {
        transform.parent = other.transform;

    }
}

void OnTriggerExit(Collider other)
{
    if (other.gameObject.tag == "platform")
    {
        transform.parent = null;

    }
}






/*
private void OnCollisionStay2D(Collision2D collision)
{
    if (collision.gameObject.name == "Platform")
    {
        transform.position = collision.transform.position;
        transform.rotation = collision.transform.rotation;
    }
}*/

/*
// faces player animation left
public void TurnLeft()
{
    if (facingLeft)
        return;
    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    facingLeft = true;
    facingRight = false;
}

//faces player animation right
public void TurnRight()
{
    if (facingRight)
        return;
    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    facingLeft = false;
    facingRight = true;
}
}
*/