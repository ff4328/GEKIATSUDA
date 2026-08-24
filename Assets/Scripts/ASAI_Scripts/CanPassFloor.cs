using UnityEngine;

/// <summary>
/// 通り抜け可能な床
/// </summary>
public class CanPassFloor : StageGimmickBase
{
    [SerializeField]
    Collider2D _col = null;

    public override void HitToCharacter(CharacterBase hitCharacter)
    {
        if (_col == null) return;
        //もし移動方向が下ならコリジョンを無効にする
        _col.enabled = false;

    }

}
