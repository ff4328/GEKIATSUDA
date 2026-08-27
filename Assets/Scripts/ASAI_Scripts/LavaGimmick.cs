using UnityEngine;

/// <summary>
/// 溶岩のダメージギミック
/// </summary>
public class LavaGimmick : DamageGimmickBase
{

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
        hitCharacter.ApplyKnockback(damage, closestPoint);

    }


}
