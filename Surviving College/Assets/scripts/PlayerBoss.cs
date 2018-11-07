using UnityEngine;
using UnityEngine.UI;

public class PlayerBoss : MonoBehaviour
{
    public Text timeUntilClass;
    public float remainingTime = 60;
    public Text gameOverText;

    private bool _isFacingRight;
    private CharacterController2D _controller;
    private float _normalizedHorizontalSpeed;

    public float MaxSpeed;
    public float SpeedAccelerationOnGround = 10f;
    public float SpeedAccelerationInAir = 5f;

    static Animator anim;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        _controller = GetComponent<CharacterController2D>();
        _isFacingRight = transform.localScale.x > 0;
    }

    void setAnimationState()
    {

        // is walking
        if (_controller.State.IsGrounded && _controller.Velocity.x != 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isJumping", false);
            anim.SetBool("isIdle", false);
        }

        // is jumping
        else if (!_controller.State.IsGrounded)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isJumping", true);
            anim.SetBool("isIdle", false);
        }

        // is idle
        else if (_controller.State.IsGrounded && (_controller.Velocity.x == 0 || _controller.Velocity.y == 0))
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isIdle", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        var movementFactor = _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;
        _controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _normalizedHorizontalSpeed = 1;
            if (!_isFacingRight)
            {
                Flip();
            }
        }

        else if (Input.GetKey(KeyCode.A))
        {
            _normalizedHorizontalSpeed = -1;
            if (_isFacingRight)
            {
                Flip();
            }
        }

        else
        {
            _normalizedHorizontalSpeed = 0;
        }

        if (_controller.CanJump && Input.GetKeyDown(KeyCode.Space))
        {
            _controller.Jump();
        }

    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _isFacingRight = transform.localScale.x > 0;
    }
}