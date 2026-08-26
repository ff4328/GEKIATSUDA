using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCharacter : MonoBehaviour
{
    public RankParameter rank;

    public int baseAttack = 3;
    public float baseSpeed = 1;
    public int baseSize = 1;

    public int finalStrongAttackPower; // 強攻撃の攻撃力
    public bool isInvincible = false;


    public CharaDataBase data;
    public CharacterMove characterMove;
    public AttackHitBox attackHitBox;

    public SpriteRenderer[] Item;

    public int finalAttackPower {  get; set; }

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
        finalStrongAttackPower = finalAttackPower * 2;
    }
    protected virtual void Update()
    {
        if (transform.position.x >= 200 || transform.position.x <= -200 || transform.position.y >= 100 || transform.position.y <= -100)
        {
            data.Dead();
            transform.position = Vector3.zero;
            characterMove.VectorToZero();
        }

        if (characterMove.IsValidAttack())
        {
            StartAttack();
            if (attackHitBox.isPowerUp==true) {
                attackHitBox.TemporaryPowerDown(20);
                Debug.Log("パワーダウン");
                attackHitBox.isPowerUp =false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("Strong Attack!");
            StartStrongAttack();
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
    void StartStrongAttack()
    {
        attackHitBox.SetAttackPower(finalStrongAttackPower);

        // 強攻撃はリーチ長めにするなど
        attackHitBox.transform.localPosition = new Vector3(1.5f, 0, 0);

        attackHitBox.SetActiveHitBox(true);

        // 強攻撃は持続長め
        Invoke(nameof(EndStrongAttack), 0.4f);
    }
    void EndStrongAttack()
    {
        attackHitBox.SetActiveHitBox(false);
    }

    public virtual void OnHit(int enemyAttack, Vector3 attackerPos)
    {
        if (isInvincible)
        {
            Item[0].enabled = true;
            return; // ★無敵ならダメージ無効
        }
        else
        {
            Item[0].enabled=false;
        }

        data.TakeDamage(enemyAttack);
        ApplyKnockback(enemyAttack, attackerPos);
    }
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
