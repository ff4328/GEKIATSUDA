using UnityEngine;

public class EnemyDummy : BaseCharacter
{
    public CharaDataBase data;

    protected override void Start()
    {
        data = new CharaDataBase();
        data.SetPercentage(0);
        data.SetAttack(0); // 敵は攻撃しない
    }

    protected override void Update()
    {
        base.Update();
        // 敵の行動はここに追加する
    }

    public override void OnHit(int enemyAttack, Vector3 attackerPos)
    {
        data.TakeDamage(enemyAttack);
        ApplyKnockback(enemyAttack, attackerPos);
        Debug.Log("敵のPercentage: " + data.Percentage);
    }

}
