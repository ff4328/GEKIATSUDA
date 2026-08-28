using Mirror;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMove : NetworkBehaviour
{
    /// <summary>
    /// ジャンプ力の定数
    /// </summary>
    const float JUMP_FORCE = 20f;

    /// <summary>
    /// 防御時間の定数
    /// </summary>
    const float GUARD_TIME = 2.0f;

    ////////////////////////////////////////////////////////////////////////////////////////////////
    
    [SerializeField]
    [Tooltip("デバッグ用のフラグ。移動を無効にする")]
    private bool _moveFlagForDebug = false;
    
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

    [SerializeField]
    [Tooltip("ガードの画像")]
    private SpriteRenderer _guardSprite;

    /// <summary>
    /// 移動速度
    /// </summary>
    public float moveSpeed = 0.1f;

    /// <summary>
    /// 移動入力格納用
    /// </summary>
    private Vector2 _moveValue;

    /// <summary>
    /// キャラクターの向き
    /// 1 = 右、-1 = 左
    /// </summary>
    private short _dir;

    private short _prevDir;

    private bool _isDashStart;

    private bool _isReallyDashStart;

    private bool _isDash;

    private int _dashCount;

    [SerializeField]
    private bool _isRepeatDash;

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
    /// 強攻撃入力してるか
    /// </summary>
    private bool _isPowerAttackInput;

    /// <summary>
    /// 防御入力してるか
    /// </summary>
    private bool _isGuardInput;

    [SerializeField]
    [SyncVar(hook = nameof(OnGuardChanged))]
    private bool _isGuard;

    /// <summary>
    /// 防御時間
    /// </summary>
    [SerializeField]
    private float _guardTime = GUARD_TIME;

    /// <summary>
    /// 最初に設定するガード画像の大きさ
    /// </summary>
    private Vector3 _guardScale;

    /// <summary>
    /// 回避してるか
    /// </summary>
    private bool _isDodge;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // キャラクターデータ生成
        _characterData = new CharaDataBase();
        //_baseCharacter = this.GetComponent<BaseCharacter>();
        //if(_baseCharacter == null)_baseCharacter = new BaseCharacter();

        // アタッチしているオブジェクトからリジッドボディ取得
        // なければ付与
        _rb = GetComponent<Rigidbody>();
        if(_rb == null)
        {
            _rb = this.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        }

        _guardSprite.enabled = false;
        Transform transform = _guardSprite.GetComponent<Transform>();
        _guardScale = transform.transform.localScale;

        // アクションの参照を保存
        _actions[(int)PlayerAction.Move] = InputSystem.actions.FindAction("Move");
        _actions[(int)PlayerAction.Jump] = InputSystem.actions.FindAction("Jump");
        _actions[(int)PlayerAction.Attack] = InputSystem.actions.FindAction("Attack");
        _actions[(int)PlayerAction.PowerAttack] = InputSystem.actions.FindAction("PowerAttack");
        _actions[(int)PlayerAction.Guard] = InputSystem.actions.FindAction("Guard");

        // 向きの初期設定
        _dir = (int)Direction.Right;
        _prevDir = _dir;

        InitDash();

        _isDodge = false;

        // 子オブジェクトから接地判定クラスを取得
        _isTouchGround = GetComponentInChildren(typeof(TouchGround)) as TouchGround;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;

        // アクション状況の更新
        _moveValue = _actions[(int)PlayerAction.Move].ReadValue<Vector2>();
        _isJumpInput = _actions[(int)PlayerAction.Jump].WasPressedThisFrame();
        _isAttackInput = _actions[(int)PlayerAction.Attack].WasPressedThisFrame();
        _isPowerAttackInput = _actions[(int)PlayerAction.PowerAttack].WasPressedThisFrame();
        _isGuardInput = _actions[(int)PlayerAction.Guard].IsPressed();
       
        IsTouchGround();
        GuardSpriteMove(_dir);
        GuardSpriteScaleChange();

        //Attack();
        Jump();
        Guard();
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer) return;

        // 実際の移動処理
        Move(_moveValue);
    }

    /// <summary>
    /// 移動アクション
    /// </summary>
    /// <param name="moveValue"></param>
    private void Move(Vector2 moveValue)
    {
        if (MoveFlagForDebug() || _isDodge) return;

        ChangeDash();

        if (!_actions[(int)PlayerAction.Move].IsPressed() || IsValidGuard())
        {
            _rb.linearVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f);
            _rb.angularVelocity = new Vector3(0f, _rb.angularVelocity.y, 0f);
            this.gameObject.transform.position = transform.position;
            return;
        }

        float x = moveValue.x * moveSpeed;
        if (_isRepeatDash) x *= 2f;
        Vector3 vector3 = new Vector3(x, 0f, 0f);
        _rb.MovePosition(transform.position + vector3);


        if (moveValue.x > 0.0f)
        {
            _dir = (int)Direction.Right;
            _rb.linearVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f);
            _rb.angularVelocity = new Vector3(0f, _rb.angularVelocity.y, 0f);
            this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if(moveValue.x < 0.0f)
        {
            _dir = (int)Direction.Left;
            _rb.linearVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f);
            _rb.angularVelocity = new Vector3(0f, _rb.angularVelocity.y, 0f);
            this.gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    private void InitDash()
    {
        _isDashStart = false;

        _isReallyDashStart = false;

        _isDash = false;

        _dashCount = 0;

        _isRepeatDash = false;
    }

    private void ChangeDash()
    {
        if(Mathf.Abs(_moveValue.x) >= 0.8f && _isGround)
            _isDashStart = true;

        if(_isDashStart) _dashCount++;

        if(_isDashStart && Mathf.Abs(_moveValue.x) < 0.8f)
            _isReallyDashStart = true;

        if (_dashCount <= 10
            && _isReallyDashStart
            && Mathf.Abs(_moveValue.x) >= 0.8f
            && _isGround
            && _prevDir == _dir)
            _isDash = true;

        _prevDir = _dir;

        RepeatDash();

        if (_isRepeatDash || (_dashCount > 10 && !_isDash))
        {
            _isDashStart = false;
            _dashCount = 0;
            _isReallyDashStart = false;
        }
    }

    private void RepeatDash()
    {
        if (_isDash && Mathf.Abs(_moveValue.x) >= 0.8f)
            _isRepeatDash = true;
        else 
        {
            _isDash = false;
            _isRepeatDash = false; 
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
        if (MoveFlagForDebug() || _isDodge) return;

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
            VectorToZero();
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
        if (_isAttackInput && !_isPowerAttackInput && !IsValidGuard()) flag = true;
        else flag = false;
        return flag;
    }

    public bool IsValidPowerAttack()
    {
        bool flag;
        if (_isPowerAttackInput && !_isAttackInput && !IsValidGuard()) flag = true;
        else flag = false;
        return flag;
    }

    [Command]
    private void CmdSetGuard(bool flag)
    {
        _isGuard = flag;
    }

    private void OnGuardChanged(bool prevFlag, bool currentFlag)
    {
        _guardSprite.enabled = currentFlag;
    }
    /// <summary>
    /// 防御アクション
    /// </summary>
    private void Guard()
    {
        if (MoveFlagForDebug() || _isDodge) return;

        if (IsValidGuard() && _guardTime != 0f)
        {
            CmdSetGuard(true);
            _guardSprite.enabled = true;
            DecreaseGuardTime();
            Dodge();
        }
        else if (IsValidGuard())
        {
            CmdSetGuard(false);
            Dodge();
        }
        else if(!IsValidGuard() && _guardSprite.enabled)
        {
            CmdSetGuard(false);
            _guardSprite.enabled = false;
        }
        else if(!_isGuardInput && !_isDodge) 
        {
            CmdSetGuard(false);
            IncreaseGuardTime();
        }
    }

    /// <summary>
    /// 防御が有効かどうか
    /// </summary>
    /// <returns></returns>
    private bool IsValidGuard()
    {
        bool flag;
        if(_isGuardInput && _isGround && !_isDodge) flag = true;
        else flag = false;
        return flag;
    }

    //private void GuardEnableProcess()
    //{
    //    Debug.Log("aaa : " + _baseCharacter.barrier.enabled);
    //    if (_baseCharacter.barrier.enabled == false)
    //    {

    //    Debug.Log("bbb");
    //    _baseCharacter.isInvincible = true;
    //    }
    //    Debug.Log("ccc");
    //}

    //private void GuardDisenableProcess()
    //{
    //    Debug.Log("ddd");
    //    if (_baseCharacter.barrier.enabled == false)
    //    {

    //        Debug.Log("eee");
    //    _baseCharacter.isInvincible = false;
    //    }
    //    Debug.Log("fff");
    //}

    private void DecreaseGuardTime()
    {
        if(_guardTime > 0f)
        {
            _guardTime -= Time.deltaTime;
        }
        else
        {
            _guardTime = 0f;
        }
    }

    private void IncreaseGuardTime()
    {
        if (_guardTime < GUARD_TIME)
        {
            _guardTime += Time.deltaTime * 0.75f;
        }
        else
        {
            _guardTime = GUARD_TIME;
        }
    }

    private void GuardSpriteScaleChange()
    {
        float guardPer = _guardTime / GUARD_TIME;
        Transform transform = _guardSprite.GetComponent<Transform>();
        transform.transform.localScale = new Vector3(_guardScale.x * guardPer, _guardScale.y * guardPer, _guardScale.z * guardPer);
    }

    private void GuardSpriteMove(short dir)
    {
        Transform transform = _guardSprite.GetComponent<Transform>();
        float x = transform.transform.localPosition.x;
        float y = transform.transform.localPosition.y;

        if (dir > 0)
        {
            transform.transform.localPosition = new Vector3(x, y, -1f);
        }
        else
        {
            transform.transform.localPosition = new Vector3(x, y, 1f);
        }
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
        if (!_moveFlagForDebug) return _moveFlagForDebug;
        
        Debug.LogWarning(gameObject + "_moveFlagforDebugがtrueになっています");
        return _moveFlagForDebug;
    }

    private void Dodge()
    {
        if (!_actions[(int)PlayerAction.Move].IsPressed()) return;

        CmdSetGuard(false);
        _isDodge = true;
        _guardSprite.enabled = false;

        Collider myCollider = this.gameObject.GetComponent<Collider>();
        myCollider.enabled = false;
        _rb.useGravity = false;

        if (Mathf.Abs(_moveValue.x) > Mathf.Abs(_moveValue.y))
        {
            if(_moveValue.x > 0.0f)
            {
                StartCoroutine(DodgeRightCoroutine(myCollider, _rb));
            }
            if(_moveValue.x < 0.0f)
            {
                StartCoroutine(DodgeLeftCoroutine(myCollider, _rb));
            }
        }
        else if(Mathf.Abs(_moveValue.x) < Mathf.Abs(_moveValue.y))
        {
            StartCoroutine(DodgeOnTheSpotCoroutine(myCollider, _rb));
        }

    }

    private IEnumerator DodgeRightCoroutine(Collider col,Rigidbody rb)
    {
        float x = transform.position.x;
        float y = transform.position.y;

        for (int i = 0; i < 20; i++)
        {
            x += 1f;
            _rb.MovePosition(new Vector3(x, y, 0f));
            yield return new WaitForSeconds(0.01f);
        }

        col.enabled = true;
        rb.useGravity = true;
        StartCoroutine(DodgeCoolTimeCoroutine());
    }

    private IEnumerator DodgeLeftCoroutine(Collider col,Rigidbody rb)
    {

        float x = transform.position.x;
        float y = transform.position.y;

        for (int i = 0; i < 20; i++)
        {
            x -= 1f;
            _rb.MovePosition(new Vector3(x, y, 0f));
            yield return new WaitForSeconds(0.01f);
        }

        col.enabled = true;
        rb.useGravity = true;
        StartCoroutine(DodgeCoolTimeCoroutine());
    }

    private IEnumerator DodgeOnTheSpotCoroutine(Collider col, Rigidbody rb)
    {
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(0.01f);
        }

        col.enabled = true;
        rb.useGravity = true;
        StartCoroutine(DodgeCoolTimeCoroutine());
    }

    private IEnumerator DodgeCoolTimeCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        _isDodge = false;
    }

    public Vector2 GetMoveValue() { return _moveValue; }

    public Vector3 GetPos() { return this.transform.position; }

    public void VectorToZero()
    {
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    public bool GetIsGuard() { return _isGuard; }
    
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
    PowerAttack,
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