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
        Debug.Log(damage);

        Vector3 direction = Vector3.zero;
        float distance = 0;

        Physics.ComputePenetration(hitCollider, hitCollider.transform.position, hitCollider.transform.rotation,
            collider, collider.transform.position, collider.transform.rotation, out direction, out distance);



        hitCharacter.ApplyKnockback(damage, closestPoint);

    }

}
