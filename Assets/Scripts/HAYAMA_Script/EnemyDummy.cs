using UnityEngine;

public class EnemyDummy : MonoBehaviour
{
    public CharaDataBase data;

    void Start()
    {
        data = new CharaDataBase();
        data.SetPercentage(0);
        data.SetAttack(0); // 敵は攻撃しない
    }

    public void OnHit(int enemyAttack, Vector3 attackerPos)
    {
        data.TakeDamage(enemyAttack);
        Knockback(enemyAttack, attackerPos);
        Debug.Log("敵のPercentage: " + data.Percentage);
    }

    void Knockback(int enemyAttack, Vector3 attackerPos)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        // 攻撃された方向（攻撃者 → 自分）
        Vector3 dir = (transform.position - attackerPos).normalized;

        // 吹っ飛び力（スマブラ風）
        float force = enemyAttack * (1 + data.Percentage * 0.05f);

        // 上方向に少し加える
        Vector3 knock = dir * force + Vector3.up * (force * 0.3f);

        rb.AddForce(knock, ForceMode.Impulse);
    }
}
