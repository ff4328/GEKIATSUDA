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

    public override void HitToCharacter(CharacterMove hitCharacter)
    {

        //ダメージを呼ぶ
        //hitCharacter.OnHit(damage);

        throw new System.NotImplementedException();

    }

}
