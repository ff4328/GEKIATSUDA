using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public RankParameter rank;

    public int baseAttack = 3;
    public float baseSpeed = 1;
    public int baseSize = 1;

    public CharaDataBase data;
    public CharacterMove characterMove;
    public AttackHitBox attackHitBox;

    public int finalAttackPower;

    protected virtual void Start()
    {
        data = new CharaDataBase();

        // ★ SwordMan の Inspector の値を BaseCharacter に反映
        if (characterMove == null)
            characterMove = GetComponent<CharacterMove>();

        if (attackHitBox == null)
            attackHitBox = GetComponentInChildren<AttackHitBox>();

        finalAttackPower = rank.GetAttack(baseAttack);

        float finalSpeed = rank.GetSpeed(baseSpeed);
        int finalSize = rank.GetSize(baseSize);

        characterMove.moveSpeed = finalSpeed * 0.1f;
        transform.localScale = Vector3.one * finalSize;
        data.SetSize(finalSize);
    }

    protected virtual void Update()
    {
        if (characterMove.IsValidAttack())
        {
            Debug.Log("Attack!");
            StartAttack();
            Debug.Log("Attack Power: " + data.Attack);
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            data.PowerUp(DataConst.POWER);
            Debug.Log("PowerUp!");
            Debug.Log("Attack Power: " + data.Attack);
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
            data.PowerDown(DataConst.POWER);
            Debug.Log("PowerDown");
            Debug.Log("Attack Power: " + data.Attack);
        attackHitBox.SetActiveHitBox(false);
    }

    public virtual void OnHit(int enemyAttack, Vector3 attackerPos){}
    public virtual void OnEnvironmentDamage(int damage)
    {
        data.TakeDamage(damage);
        ApplyKnockback(damage, -transform.position);
    }
    public virtual void ApplyKnockback(int power, Vector3 attackerPos)
    {
        var rb = GetComponent<Rigidbody>();
        if (rb == null) return;

        Vector3 dir = (transform.position - attackerPos).normalized;
        float force = power * (1 + data.Percentage * 0.05f);

        Vector3 knock = dir * force + Vector3.up * (force * 0.3f);

        rb.AddForce(knock, ForceMode.Impulse);
    }
}
