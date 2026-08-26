using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCharacter : MonoBehaviour
{
    public RankParameter rank;

    public int baseAttack = 3;
    public float baseSpeed = 1;
    public int baseSize = 1;

    public int finalStrongAttackPower; // 強攻撃の攻撃力


    public CharaDataBase data;
    public CharacterMove characterMove;
    public AttackHitBox attackHitBox;

    public SpriteRenderer barrier;
    public bool isInvincible = false;

    public int finalAttackPower { get; set; }

    //鈴木
    private Hit_Effect  effect;

    protected virtual void Start()
    {

        //鈴木＝＝＝＝＝＝＝＝
        //書くとここじゃなかったらごめん
        EffectManager manager =
             FindFirstObjectByType<EffectManager>();

        effect = new Hit_Effect(manager);
        //＝＝＝＝＝＝＝＝＝＝

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
            if (attackHitBox.isPowerUp == true)
            {
                attackHitBox.TemporaryPowerDown(20);
                attackHitBox.isPowerUp = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
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
        if (isInvincible) return;
        data.TakeDamage(enemyAttack);
        ApplyKnockback(enemyAttack, attackerPos);
        //鈴木
        effect.Hit(attackerPos);
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

        // ★ 攻撃方向（上下成分も使う）
        Vector3 dir = (transform.position - attackerPos).normalized;

        float force = power * (1 + data.Percentage * 0.05f);

        // ★ 斜めに飛ぶ自然なノックバック
        Vector3 knock = dir * force;

        // ★ 少しだけ上方向を足す（スマブラ風）
        knock += Vector3.up * (force * 0.2f);

        rb.AddForce(knock, ForceMode.Impulse);
    }

    public void StartInvincible(float duration)
    {
        isInvincible = true;

        if (barrier != null)
            barrier.enabled = true;

        Invoke(nameof(EndInvincible), duration);
    }

    void EndInvincible()
    {
        isInvincible = false;

        if (barrier != null)
            barrier.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StageGimmick")
        {
            other.GetComponent<StageGimmickBase>().HitToCharacter(this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "StageGimmick")
        {
            if (other.GetComponent<StageGimmickBase>().IsDamageGimmick()) return;
            other.GetComponent<StageGimmickBase>().HitToCharacter(this);
        }
    }
}