using UnityEngine;

/// <summary>
/// 落ちる床ギミック
/// </summary>
public class FallFloor : StageGimmickBase
{
    //移動方向
    private readonly Vector3 _MOVE_DIRECTION = new Vector3(0.0f, -1.0f, 0.0f);
    //落下速度の加算
    [SerializeField]
    private float _additionalFallSpeed = 20.0f;
    //落下の最大速度
    private readonly float _MAX_FALL_SPEED = 5000;
    //落下速度
    private float _fallSpeed = 0;

    //落下待機時間
    private const float _FALL_STAY_TIME_SEC_ = 0.5f;
    //落下待機のタイマー
    private float _fallStayTimerSec = 0.0f;

    //初期位置
    private Vector3 _initialPos = Vector3.zero;
    //初期の色
    private Color _initialColor = Color.white;
    //復活までの時間
    private const float _RESURRECTION_TIME = 7.0f;
    //復活のタイマー
    private float _resurrectionTimer = 0.0f;

    //FallFloorの状態
    private enum State
    {
        None,       //落下していない状態
        StayFall,   //落下するまでの待機状態
        Fall,       //落下状態
    }
    //現在の状態
    private State _state = State.None;

    protected override void Start()
    {
        base.Start();
        //初期位置を保存
        _initialPos = transform.position;
        //初期の色を保存
        _initialColor = GetComponent<MeshRenderer>().material.color;
    }

    private void Update()
    {
        //落下していない状態なら処理しない
        if (_state == State.None) return;

        float deltaTime = Time.deltaTime;

        //落下待機中の処理
        if ( _state == State.StayFall)
        {
            //タイマーを加算
            _fallStayTimerSec += deltaTime;

            //待機時間終了でなければ処理しない
            if (_fallStayTimerSec >= _FALL_STAY_TIME_SEC_)
            {
                //状態を変更
                _state = State.Fall;
                //初期化
                _fallStayTimerSec = 0.0f;

            }


        }
        //落下中の処理
        else if (_state == State.Fall)
        {

            //落下速度を加算
            _fallSpeed += _additionalFallSpeed * deltaTime;
            //落下速度の上限対策
            _fallSpeed = Mathf.Min(_fallSpeed, _MAX_FALL_SPEED);

            //復活までの時間を加算
            _resurrectionTimer += deltaTime;

            //復活時間になったら
            if (_resurrectionTimer >= _RESURRECTION_TIME)
            {
                //状態を変更
                _state = State.None;

                //初期化
                _resurrectionTimer = 0.0f;
                _fallSpeed = 0.0f;
                //初期座標に戻す
                transform.position = _initialPos;
                //初期の色に戻す
                GetComponent<MeshRenderer>().material.color = _initialColor;
            }

        }

    }

    private void FixedUpdate()
    {
        //落下状態でなければ処理しない
        if (_state != State.Fall) return;
        //落下
        transform.position += _MOVE_DIRECTION * Time.fixedDeltaTime * _fallSpeed;
    }

    public override void HitToCharacter(BaseCharacter hitCharacter)
    {
        //落下していない状態でなければ
        if (_state != State.None) return;

        //落下待機状態に変更
        _state = State.StayFall;

        //色の変更
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

}
