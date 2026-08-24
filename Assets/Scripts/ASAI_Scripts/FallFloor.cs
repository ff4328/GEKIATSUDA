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


    private void Update()
    {
        //落下中でなければ処理をしない
        if (!_isFalling) return;

        //落下速度を加算
        _fallSpeed += _ADDITIONAL_FALL_SPEED * Time.deltaTime;
        //落下速度の上限対策
        _fallSpeed = Mathf.Min(_fallSpeed, _MAX_FALL_SPEED);
    }

    private void FixedUpdate()
    {
        //落下中でなければ処理をしない
        if (!_isFalling) return;
        //落下
        transform.position += _MOVE_DIRECTION * Time.deltaTime * _fallSpeed;
    }

    public override void HitToCharacter(CharacterBase hitCharacter)
    {
        //落下開始
        _isFalling = true;

    }

}
