using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private Animator _animator;
    private bool _isGrounded;
    private Rigidbody2D _rb2d;
    private int _facingDirection = 1; // 1: phải, -1: trái
    private GameManager _gameManager;
    private AudioManager _audioManager;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _gameManager = FindAnyObjectByType<GameManager>();
        _audioManager = FindAnyObjectByType<AudioManager>();
    }

    private void Update()
    {
        if (_gameManager.IsGameOver()) return;
        HandleMovement();
        HandleJump();
        UpdateAnimation();
    }

    private void HandleMovement()
    {
        var moveInput = Input.GetAxis("Horizontal");
        _rb2d.linearVelocity = new Vector2(moveInput * moveSpeed, _rb2d.linearVelocity.y);

        if (moveInput > 0 && _facingDirection != 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
            _facingDirection = 1;
        }
        else if (moveInput < 0 && _facingDirection != -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            _facingDirection = -1;
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _audioManager.PlayJumpSound();
            _rb2d.linearVelocity = new Vector2(_rb2d.linearVelocity.x, jumpForce);
        }

        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void UpdateAnimation()
    {
        var isRunning = Mathf.Abs(_rb2d.linearVelocity.x) > 0.1f;
        var isJumping = !_isGrounded;
        _animator.SetBool(IsRunning, isRunning);
        _animator.SetBool(IsJumping, isJumping);
    }
}