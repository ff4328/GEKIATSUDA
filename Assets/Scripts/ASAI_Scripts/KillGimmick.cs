using UnityEngine;

/// <summary>
/// 即死のダメージギミック
/// </summary>
public class KillGimmick : DamageGimmickBase
{

    public override void HitToCharacter(BaseCharacter hitCharacter)
    {

        //当たったキャラクターを即死させる
        hitCharacter.Deads();

    }

}
