using UnityEngine;

public class SwordMan : BaseCharacter
{
    public AttackHitBox attackHitBox;
    public CharacterMove characterMove;

    public bool isStunned = false;
    public bool isKnockback = false;

    private Rigidbody rb;

    protected override void Start()
    {
        // ★職業ごとの最低値
        baseAttack = 5;
        baseSpeed = 1;
        baseSize = 1;

        // ★BaseCharacter の初期化（data, characterMove）
        base.Start();

        // ★SwordMan 固有の初期化
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isStunned || isKnockback) return;

        if (characterMove.IsValidAttack())
        {
            StartAttack();
        }
    }

    void StartAttack()
    {
        attackHitBox.SetAttackPower(finalAttackPower); // ★攻撃力を渡す
        attackHitBox.transform.localPosition = new Vector3(1, 0, 0);
        attackHitBox.SetActiveHitBox(true);
        Invoke(nameof(EndAttack), 0.2f);
    }

    void EndAttack()
    {
        attackHitBox.SetActiveHitBox(false);
    }

    public override void OnHit(int enemyAttack, Vector3 attackerPos)
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
