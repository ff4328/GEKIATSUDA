using UnityEngine;

/// <summary>
/// ステージギミックの基底
/// </summary>
public abstract class StageGimmickBase : MonoBehaviour
{

    protected virtual void Start()
    {
    }

    /// <summary>
    /// キャラクターと衝突した時の処理
    /// </summary>
    public abstract void HitToCharacter(/* Character character*/);

    /// <summary>
    /// 衝突方向を計算
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    protected Vector3 CalculateCollisionDirection(Vector3 position)
    {
        //衝突
        Vector3 Direction = position - transform.position;
        //正規化
        Direction = Direction.normalized;
        return Direction;
    }

    /// <summary>
    /// ギミックのアクティブ状態を設定
    /// </summary>
    /// <param name="active"></param>
    void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

}
