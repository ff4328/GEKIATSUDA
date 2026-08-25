using UnityEngine;

public class SwordMan : MonoBehaviour
{
    public CharaDataBase data;
    public AttackHitBox attackHitBox;
    public CharacterMove characterMove;

    public bool isStunned = false;     // 食らって動けない
    public bool isKnockback = false;   // 吹っ飛び中は動けない

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        data = new CharaDataBase();
        data.SetAttack(10);
    }

    void Update()
    {
        // スタン or 吹っ飛び中は操作禁止
        if (isStunned || isKnockback)
        {
            return;
        }
        if (characterMove.IsValidAttack())
        {
            StartAttack();
        }
    }
    void StartAttack()
    {
        attackHitBox.transform.localPosition = new Vector3(1, 0, 0);
        attackHitBox.SetActiveHitBox(true);
        Invoke(nameof(EndAttack), 0.2f);
    }


    void EndAttack()
    {
        attackHitBox.SetActiveHitBox(false);
    }
    public void OnHit(int enemyAttack, Vector3 attackerPos)
    {
        data.TakeDamage(enemyAttack);

        Debug.Log($"ダメージ受けた: {enemyAttack}, 現在のPercentage: {data.Percentage}");

        ApplyStun(0.3f);
        Knockback(enemyAttack, attackerPos);
    }


    void ApplyStun(float duration)
    {
        isStunned = true;
        Invoke(nameof(EndStun), duration);
    }

    void EndStun()
    {
        isStunned = false;
    }

    void Knockback(int enemyAttack, Vector3 attackerPos)
    {
        isKnockback = true;

        Vector3 dir = (transform.position - attackerPos).normalized;
        float force = enemyAttack * (1 + data.Percentage * 0.05f);

        Vector3 knock = dir * force + Vector3.up * (force * 0.3f);

        rb.AddForce(knock, ForceMode.Impulse);

        Invoke(nameof(EndKnockback), 0.3f);
    }

    void EndKnockback()
    {
        isKnockback = false;
    }
}
