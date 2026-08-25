using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMove : MonoBehaviour
{
    /// <summary>
    /// 移動速度の定数
    /// </summary>
    const float MOVE_SPEED = 0.1f;

    /// <summary>
    /// ジャンプ力の定数
    /// </summary>
    const float JUMP_FORCE = 20f;

    ////////////////////////////////////////////////////////////////////////////////////////////////
    
    [SerializeField]
    [Tooltip("デバッグ用のフラグ。移動を無効にする")]
    private bool _moveFlagforDebug = false;
    
    ////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// InputAction配列 各アクションの参照を格納する
    /// </summary>
    private InputAction[] _actions = new InputAction[(int)PlayerAction.Max];

    /// <summary>
    /// Rigidbody格納用の変数
    /// </summary>
    private Rigidbody _rb = null;

    /// <summary>
    /// キャラクターデータ
    /// このクラスと継承したクラスのみ値を変えられる
    /// </summary>
    public CharaDataBase _characterData { get; protected set; } = null;

    /// <summary>
    /// 移動入力格納用
    /// </summary>
    private Vector2 _moveValue;

    /// <summary>
    /// キャラクターの向き
    /// 1 = 右、-1 = 左
    /// </summary>
    private short _dir;

    /// <summary>
    /// ジャンプ入力してるか
    /// </summary>
    private bool _isJumpInput;

    /// <summary>
    /// ダブルジャンプ可能か
    /// </summary>
    private bool _isDoubleJump;

    /// <summary>
    /// 接地してるかクラス
    /// </summary>
    private TouchGround _isTouchGround;

    /// <summary>
    /// 接地してるか
    /// </summary>
    private bool _isGround;

    /// <summary>
    /// 攻撃入力してるか
    /// </summary>
    private bool _isAttackInput;

    /// <summary>
    /// 防御入力してるか
    /// </summary>
    private bool _isGuardInput;

    /// <summary>
    /// 回避してるか
    /// </summary>
    private bool _isDodge;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // キャラクターデータ生成
        _characterData = new CharaDataBase();

        // アタッチしているオブジェクトからリジッドボディ取得
        // なければ付与
        _rb = GetComponent<Rigidbody>();
        if(_rb == null)
        {
            _rb = this.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        }

        // アクションの参照を保存
        _actions[(int)PlayerAction.Move] = InputSystem.actions.FindAction("Move");
        _actions[(int)PlayerAction.Jump] = InputSystem.actions.FindAction("Jump");
        _actions[(int)PlayerAction.Attack] = InputSystem.actions.FindAction("Attack");
        _actions[(int)PlayerAction.Guard] = InputSystem.actions.FindAction("Guard");

        // 向きの初期設定
        _dir = (int)Direction.Right;

        // 子オブジェクトから接地判定クラスを取得
        _isTouchGround = GetComponentInChildren(typeof(TouchGround)) as TouchGround;
    }

    // Update is called once per frame
    void Update()
    {
        // アクション状況の更新
        _moveValue = _actions[(int)PlayerAction.Move].ReadValue<Vector2>();
        _isJumpInput = _actions[(int)PlayerAction.Jump].WasPressedThisFrame();
        _isAttackInput = _actions[(int)PlayerAction.Attack].WasPressedThisFrame();
        _isGuardInput = _actions[(int)PlayerAction.Guard].IsPressed();
       
        IsTouchGround();

        //Attack();
        Jump();
        Guard();
    }

    private void FixedUpdate()
    {
        // 実際の移動処理
        Move(_moveValue);
    }

    /// <summary>
    /// 移動アクション
    /// </summary>
    /// <param name="moveValue"></param>
    private void Move(Vector2 moveValue)
    {
        if (MoveFlagForDebug()) return;

        if (!_actions[(int)PlayerAction.Move].IsPressed() || IsValidGuard())
        {
            _rb.linearVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f);
            _rb.angularVelocity = new Vector3(0f, _rb.angularVelocity.y, 0f);
            this.gameObject.transform.position = transform.position;
            return;
        }

        float x = moveValue.x * MOVE_SPEED;
        this.gameObject.transform.position += new Vector3(x, 0f, 0f);

        if(moveValue.x >= 0.0f)
        {
            _dir = (int)Direction.Right;
            _rb.linearVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f);
            _rb.angularVelocity = new Vector3(0f, _rb.angularVelocity.y, 0f);
            this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            _dir = (int)Direction.Left;
            _rb.linearVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f);
            _rb.angularVelocity = new Vector3(0f, _rb.angularVelocity.y, 0f);
            this.gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    /// <summary>
    /// 接地判定
    /// </summary>
    private void IsTouchGround()
    {
        _isGround = _isTouchGround.isGround;
        _isDoubleJump = _isTouchGround.isDoubleJump;
    }

    /// <summary>
    /// ジャンプアクション
    /// </summary>
    private void Jump()
    {
        if (MoveFlagForDebug()) return;

        if (_isJumpInput && _isGround)
        {
            _rb.AddForce(new Vector3(0f, JUMP_FORCE, 0f), ForceMode.Impulse);
        }

        DoubleJump();
    }

    /// <summary>
    /// ダブルジャンプアクション
    /// </summary>
    private void DoubleJump()
    {
        if (_isJumpInput && !_isGround && _isDoubleJump)
        {
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _rb.AddForce(new Vector3(0f, JUMP_FORCE, 0f), ForceMode.Impulse);
            _isTouchGround.isDoubleJump = false;
        }
    }

    ///// <summary>
    ///// 攻撃アクション
    ///// </summary>
    //private void Attack()
    //{
        
    //    if (_isAttackInput && !IsValidGuard())
    //    {
    //        //GameObject aa = Instantiate(_attackArea, (this.transform.position + new Vector3((1f*_dir),0.0f,0.0f)), Quaternion.identity);
    //        //aa.transform.parent = this.transform;
    //        //Destroy(aa, 0.25f);
    //    }
    //}

    public bool IsValidAttack()
    {
        bool flag;
        if (_isAttackInput && !IsValidGuard()) flag = true;
        else flag = false;
        return flag;
    }

    /// <summary>
    /// 防御アクション
    /// </summary>
    private void Guard()
    {
        if (MoveFlagForDebug()) return;

        if (IsValidGuard())
        {
            //_moveValue = Vector2.zero;
            Dodge();
        }
    }

    /// <summary>
    /// 防御が有効かどうか
    /// </summary>
    /// <returns></returns>
    private bool IsValidGuard()
    {
        bool flag;
        flag = _isGuardInput && _isGround;
        return flag;
    }

    public Collider[] GetColliders()
    {
        Collider[] colliders = new Collider[(int)MyCollider.Max];
        colliders[(int)MyCollider.MySelf] = this.gameObject.GetComponent<Collider>();
        colliders[(int)MyCollider.FootStep] = _isTouchGround.GetFootStepCollider();

        return colliders;
    }

    private bool MoveFlagForDebug()
    {
        if (!_moveFlagforDebug) return _moveFlagforDebug;
        
        Debug.LogWarning(gameObject + "_moveFlagforDebugがtrueになっています");
        return _moveFlagforDebug;
    }

    private void Dodge()
    {
        if (!_actions[(int)PlayerAction.Move].IsPressed()) return;

        Collider myCollider = this.gameObject.GetComponent<Collider>();
        myCollider.enabled = false;
        Material material = GetComponent<Renderer>().material;
        material.color = Color.purple;
        _rb.useGravity = false;

        if (Mathf.Abs(_moveValue.x) > Mathf.Abs(_moveValue.y))
        {
            if(_moveValue.x > 0.0f)
            {
                StartCoroutine(DodgeRightCoroutine(myCollider, material, _rb));
            }
            if(_moveValue.x < 0.0f)
            {
                StartCoroutine(DodgeLeftCoroutine(myCollider, material, _rb));
            }
        }
        else if(Mathf.Abs(_moveValue.x) < Mathf.Abs(_moveValue.y))
        {
            StartCoroutine(DodgeOnTheSpotCoroutine(myCollider, material, _rb));
        }

    }

    private IEnumerator DodgeRightCoroutine(Collider col, Material mat, Rigidbody rb)
    {
        float x = transform.position.x;
        float y = transform.position.y;

        for (int i = 0; i < 20; i++)
        {
            x += 0.10f;
            _rb.MovePosition(new Vector3(x, y, 0f));
            yield return new WaitForSeconds(0.01f);
        }

        col.enabled = true;
        mat.color= Color.gray;
        rb.useGravity = true;
    }

    private IEnumerator DodgeLeftCoroutine(Collider col, Material mat, Rigidbody rb)
    {

        float x = transform.position.x;
        float y = transform.position.y;

        for (int i = 0; i < 20; i++)
        {
            x -= 0.10f;
            _rb.MovePosition(new Vector3(x, y, 0f));
            yield return new WaitForSeconds(0.01f);
        }

        col.enabled = true;
        mat.color = Color.gray;
        rb.useGravity = true;
    }

    private IEnumerator DodgeOnTheSpotCoroutine(Collider col, Material mat, Rigidbody rb)
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.01f);
        }

        col.enabled = true;
        mat.color = Color.gray;
        rb.useGravity = true;
    }
}

/// <summary>
/// アクション一覧列挙体
/// </summary>
enum PlayerAction
{
    None,
    Move,
    Jump,
    Attack,
    Guard,
    Max
}

/// <summary>
/// 向き列挙体
/// </summary>
enum Direction
{
    Left = -1,
    Right = 1
}

enum MyCollider
{
    MySelf,
    FootStep,
    Max
}