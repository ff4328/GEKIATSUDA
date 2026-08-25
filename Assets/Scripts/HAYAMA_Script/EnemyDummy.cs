using UnityEngine;

public class EnemyDummy : BaseCharacter
{
    public CharaDataBase data;

    void Start()
    {
        data = new CharaDataBase();
        data.SetPercentage(0);
        data.SetAttack(0); // 敵は攻撃しない
    }

    public override void OnHit(int enemyAttack, Vector3 attackerPos)
    {
        data.TakeDamage(enemyAttack);
        ApplyKnockback(enemyAttack, attackerPos);
        Debug.Log("敵のPercentage: " + data.Percentage);
    }

}
