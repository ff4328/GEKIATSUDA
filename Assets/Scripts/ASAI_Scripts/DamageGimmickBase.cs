using UnityEngine;

/// <summary>
/// ダメージギミックの基底
/// </summary>
public abstract class DamageGimmickBase : StageGimmickBase
{

    protected int damage = 0;

    public abstract override void HitToCharacter(BaseCharacter hitCharacter);

}
