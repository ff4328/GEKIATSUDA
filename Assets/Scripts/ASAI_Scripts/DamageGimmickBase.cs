using UnityEngine;

/// <summary>
/// ダメージギミックの基底
/// </summary>
public abstract class DamageGimmickBase : StageGimmickBase
{

    protected float damage = 0.0f;

    public abstract override void HitToCharacter(CharacterBase hitCharacter);

}
