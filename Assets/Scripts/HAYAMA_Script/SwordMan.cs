using UnityEngine;

public class SwordMan : BaseCharacter
{
    public AttackHitBox attackHitBox;
    public CharacterMove characterMove;
    [SerializeField]CharaDataBase data;

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
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            data.Heal(100);
            Debug.Log(data.Percentage);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            data.TakeDamage(10);
            Debug.Log(data.Percentage);
        }

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

        ApplyStun(0.3f);

        // ★共通ノックバックを使う
        ApplyKnockback(enemyAttack, attackerPos);
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

    void EndKnockback()
    {
        isKnockback = false;
    }
}
