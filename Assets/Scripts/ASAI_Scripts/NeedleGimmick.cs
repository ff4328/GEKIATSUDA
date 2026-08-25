using UnityEngine;

/// <summary>
/// とげのギミック
/// </summary>
public class NeedleGimmick : DamageGimmickBase
{

    private void Start()
    {
        damage = 10;
    }

    public override void HitToCharacter(CharacterMove hitCharacter)
    {

        //ダメージを呼ぶ
        //hitCharacter.OnHit(damage);

        throw new System.NotImplementedException();

    }

}
