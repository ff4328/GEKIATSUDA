using UnityEngine;

/// <summary>
/// 溶岩のダメージギミック
/// </summary>
public class LavaGimmick : DamageGimmickBase
{
    //下のマグマか
    [SerializeField]
    private bool _isUnder = true;

    protected override void Start()
    {
        base.Start();
        damage = 20;
    }

    public override void HitToCharacter(BaseCharacter hitCharacter)
    {
        //衝突したキャラクターのコライダーを入手
        Collider hitCollider = hitCharacter.GetComponent<Collider>();
        //最近接点を求める
        Vector3 closestPoint = collider.ClosestPoint(hitCollider.bounds.center);

        //下からあたる前提

        //めり込んでいたら
        if (hitCharacter.transform.position.y < closestPoint.y && _isUnder)
        {
            //めり込み量を調べる
            float overlapY = hitCharacter.transform.position.y - closestPoint.y;
            //めり込み量を足す
            closestPoint.y += overlapY;

        }

        hitCharacter.OnHit(damage, closestPoint);

    }


}
