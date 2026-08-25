using UnityEngine;

/// <summary>
/// ステージギミックの基底
/// </summary>
public abstract class StageGimmickBase : MonoBehaviour
{
    [SerializeField]
    protected Collider collider = null;

    protected virtual void Start()
    {
        if(collider == null)
        {
            Debug.Log("StageGimmickのコライダーをセットしていない");
            collider = gameObject.AddComponent<BoxCollider>();
        }

    }

    /// <summary>
    /// キャラクターと衝突した時の処理
    /// </summary>
    public abstract void HitToCharacter(CharacterBase hitCharacter);

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
    protected void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    /// <summary>
    /// コライダーの有効、無効を設定
    /// </summary>
    /// <param name="enable"></param>
    protected void SetEnabledCollider(bool enable)
    {
        collider.enabled = enable;
    }

}
