using UnityEngine;

/// <summary>
/// とげのギミック
/// </summary>
public class NeedleGimmick : DamageGimmickBase
{

    protected override void Start()
    {
        base.Start();
        damage = 10;
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
