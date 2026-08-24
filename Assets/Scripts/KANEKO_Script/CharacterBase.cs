using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterBase : MonoBehaviour
{
    const float MOVE_SPEED = 0.1f;
    const float JUMP_FORCE = 20f;

    private InputAction[] _actions = new InputAction[(int)PlayerAction.Max];
    private Rigidbody _rb = null;
    private Vector2 _moveValue;
    private short _dir;    // true = 右、false = 左
    private bool _isJump;
    private bool _isDoubleJump;
    private TouchGround _isTouchGround;
    private bool _isGround;
    private bool _isAttack;
    [SerializeField]
    private GameObject _attackArea;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if(_rb == null)
        {
            _rb = this.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        }
        _actions[(int)PlayerAction.Move] = InputSystem.actions.FindAction("Move");
        _actions[(int)PlayerAction.Jump] = InputSystem.actions.FindAction("Jump");
        _actions[(int)PlayerAction.Attack] = InputSystem.actions.FindAction("Attack");

        _dir = (int)Direction.Right;
        _isTouchGround = GetComponentInChildren(typeof(TouchGround)) as TouchGround;
    }

    // Update is called once per frame
    void Update()
    {

        _moveValue = _actions[(int)PlayerAction.Move].ReadValue<Vector2>();
        _isJump = _actions[(int)PlayerAction.Jump].WasPressedThisFrame();
        _isAttack = _actions[(int)PlayerAction.Attack].WasPressedThisFrame();
       
        IsTouchGround();

        Attack();
        Jump();
        Debug.Log(_isGround + " : _isGround");
    }

    private void FixedUpdate()
    {
        Move(_moveValue);
    }

    private void Move(Vector2 moveValue)
    {
        Debug.Log(moveValue);
        float x = moveValue.x * MOVE_SPEED;
        this.gameObject.transform.position += new Vector3(x, 0f, 0f);

        if (!_actions[(int)PlayerAction.Move].IsPressed()) return;
        if(moveValue.x >= 0.0f)
        {
            _dir = (int)Direction.Right;
            this.gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
        }
        else
        {
            _dir = (int)Direction.Left;
            this.gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
        }
    }

    private void IsTouchGround()
    {
        _isGround = _isTouchGround.isGround;
        _isDoubleJump = _isTouchGround.isDoubleJump;
    }

    private void Jump()
    {
        Debug.Log(_isJump);
        if (_isJump && _isGround)
        {
            _rb.AddForce(new Vector3(0f, JUMP_FORCE, 0f), ForceMode.Impulse);
        }

        DoubleJump();
    }

    private void DoubleJump()
    {
        if (_isJump && !_isGround && _isDoubleJump)
        {
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _rb.AddForce(new Vector3(0f, JUMP_FORCE, 0f), ForceMode.Impulse);
            _isTouchGround.isDoubleJump = false;
            Debug.Log("Double Jumped.");
        }
    }

    private void Attack()
    {
        if (_isAttack)
        {
            GameObject aa = Instantiate(_attackArea, (this.transform.position + new Vector3((1f*_dir),0.0f,0.0f)), Quaternion.identity);

            Destroy(aa, 0.25f);
        }
    }
}

enum PlayerAction
{
    None,
    Move,
    Jump,
    Attack,
    Max
}

enum Direction
{
    Left = -1,
    Right = 1
}