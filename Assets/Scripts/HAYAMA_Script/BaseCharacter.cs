using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Timeline;

public class BaseCharacter : NetworkBehaviour
{
    public RankParameter rank;

    public int baseAttack = 3;
    public float baseSpeed = 1;
    public int baseSize = 1;

    public int finalStrongAttackPower; // 強攻撃の攻撃力

    [SerializeField] protected Vector3 attackOffset;
    [SerializeField] protected Vector3 strongAttackOffset;

    public CharaDataBase data;
    public CharacterMove characterMove;
    public AttackHitBox attackHitBox;

    public SpriteRenderer barrier;
    public bool isInvincible = false;

    public int finalAttackPower { get; set; }

    //鈴木
    private Hit_Effect effect;

[SyncVar]
private float _percentage;

    public float Percentage => _percentage;

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


        data.Start();
    }
    protected virtual void Update()
    {
        // Debug.Log(data.LaunchRate);

        if (transform.position.x >= 280 || transform.position.x <= -280 || transform.position.y >= 140 || transform.position.y <= -140)
        {
            Deads();
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

        if (characterMove.IsValidPowerAttack())
        {
            StartStrongAttack();
            data.SmashDead();
        }
    }

    void StartAttack()
    {
        attackHitBox.SetAttackPower(finalAttackPower); // ★攻撃力を渡す
        attackHitBox.transform.localPosition = attackOffset;
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
        attackHitBox.transform.localPosition = strongAttackOffset;

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
        if (isInvincible || characterMove.GetIsGuard()) return;
        data.TakeDamage(enemyAttack);

        if (!isServer)
            return;

        _percentage = data.Percentage;

        ApplyKnockback(enemyAttack, attackerPos);
        //鈴木
        effect.Hit(attackerPos);
    }

    public virtual void OnEnvironmentDamage(int damage)
    {
        if (!isServer)
            return;

        data.TakeDamage(damage);

        _percentage = data.Percentage;

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

    public void AddRank(int attackUp, int speedUp, int sizeUp)
    {
        // ランクを増減
        rank.attackRank += attackUp;
        rank.speedRank += speedUp;
        rank.sizeRank += sizeUp;

        // ランクの範囲を制限（1〜5）
        rank.attackRank = Mathf.Clamp(rank.attackRank, 1, 5);
        rank.speedRank = Mathf.Clamp(rank.speedRank, 1, 5);
        rank.sizeRank = Mathf.Clamp(rank.sizeRank, 1, 5);

        // 再計算
        UpdateFinalStats();
    }
    public void UpdateFinalStats()
    {
        finalAttackPower = rank.GetAttack(baseAttack);

        float finalSpeed = rank.GetSpeed(baseSpeed);
        int finalSize = rank.GetSize(baseSize);

        characterMove.moveSpeed = finalSpeed * 0.1f;
        transform.localScale = Vector3.one * finalSize;
    }
    public void AddAttackRank(int value)
    {
        rank.attackRank += value;
        rank.attackRank = Mathf.Clamp(rank.attackRank, 1, 5);
        UpdateFinalStats();
    }
    public void AddSpeedRank(int value)
    {
        rank.speedRank += value;
        rank.speedRank = Mathf.Clamp(rank.speedRank, 1, 5);
        UpdateFinalStats();
    }

    public void AddSizeRank(int value)
    {
        rank.sizeRank += value;
        rank.sizeRank = Mathf.Clamp(rank.sizeRank, 1, 5);
        UpdateFinalStats();
    }
    public void ResetRank()
    {
        rank.attackRank = 1;
        rank.speedRank = 1;
        rank.sizeRank = 1;
        UpdateFinalStats();
    }

    public void Deads()
    {
        data.Dead();
        transform.position = Vector3.zero;
        characterMove.VectorToZero();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        Debug.Log($"BattlePlayer Spawn完了: {name}");
    }
}