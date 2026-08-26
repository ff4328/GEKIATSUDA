using UnityEngine;

/// <summary>
/// ダメージギミックの基底
/// </summary>
public abstract class DamageGimmickBase : StageGimmickBase
{

    protected int damage = 0;

    protected override void Start()
    {
        base.Start();
    }

    public abstract override void HitToCharacter(BaseCharacter hitCharacter);

}
