using UnityEngine;

/// <summary>
/// 落ちる床ギミック
/// </summary>
public class FallFloor : StageGimmickBase
{
    //移動方向
    private readonly Vector3 _MOVE_DIRECTION = new Vector3(0.0f, -1.0f, 0.0f);
    //落下速度の加算量
    private const float _ADDITIONAL_FALL_SPEED = 10.0f;
    //落下の最大速度
    private const float _MAX_FALL_SPEED = 20.0f;
    //落下速度
    private float _fallSpeed = 0;
    //落下中か判定
    private bool _isFalling = false;

    //初期位置
    private Vector3 _initialPos = Vector3.zero;
    //復活までの時間
    private const float __RESURRECTION_TIME = 15.0f;
    //復活のタイマー
    private float _resurrectionTimer = 0.0f;

    protected override void Start()
    {
        base.Start();
        //初期位置を保存
        _initialPos = transform.position;
    }

    private void Update()
    {
        //落下中でなければ処理をしない
        if (!_isFalling) return;

        float deltaTime = Time.deltaTime;

        //落下速度を加算
        _fallSpeed += _ADDITIONAL_FALL_SPEED * deltaTime;
        //落下速度の上限対策
        _fallSpeed = Mathf.Min(_fallSpeed, _MAX_FALL_SPEED);

        //復活までの時間を加算
        _resurrectionTimer += deltaTime;

        //復活時間になったら
        if (_resurrectionTimer > __RESURRECTION_TIME)
        {
            //初期化
            _isFalling = false;
            _resurrectionTimer = 0.0f;
            //初期座標に戻す
            transform.position = _initialPos;
        }

    }

    private void FixedUpdate()
    {
        //落下中でなければ処理をしない
        if (!_isFalling) return;
        //落下
        transform.position += _MOVE_DIRECTION * Time.deltaTime * _fallSpeed;
    }

    public override void HitToCharacter(BaseCharacter hitCharacter)
    {
        //落下してたら処理しない
        if (_isFalling) return;
        //落下開始
        _isFalling = true;

    }

}
