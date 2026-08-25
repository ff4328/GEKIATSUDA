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

        //ダメージを呼ぶ
        hitCharacter.OnEnvironmentDamage(damage);

        hitCharacter.ApplyKnockback(damage, transform.position);

    }

}
