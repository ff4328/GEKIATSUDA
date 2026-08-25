using UnityEngine;

public class SwordMan : CharacterMove
{
    public CharaDataBase data;
    public AttackHitBox attackHitBox;

    private bool isAttacking = false;

    void Start()
    {
        data = new CharaDataBase();
        SetUp();

        attackHitBox.owner = this;
        attackHitBox.SetActiveHitBox(false);

        Debug.Log("SwordMan 初期化完了");
    }

    void SetUp()
    {
        data.SetPercentage(0);
        data.SetAttack(3);
        data.SetSpeed(1);
        data.SetSize(1);
        data.SetJumpPower(2);

        Debug.Log("ステータス設定完了");
    }

    void Update()
    {
        if (IsValidAttack())
        {
            StartAttack();
        }
    }

    void StartAttack()
    {
        Debug.Log("攻撃開始");
        isAttacking = true;
        attackHitBox.SetActiveHitBox(true);

        // 0.2秒後に消す
        Invoke(nameof(EndAttack), 0.2f);
    }

    void EndAttack()
    {
        Debug.Log("攻撃終了");
        isAttacking = false;
        attackHitBox.SetActiveHitBox(false);
    }

    public void OnHit(int enemyAttack)
    {
        Debug.Log("ダメージ受けた: " + enemyAttack);

        // Percentage を増やす
        data.TakeDamage(enemyAttack);

        // 吹っ飛び処理
        Knockback(enemyAttack);

        Debug.Log("現在のPercentage: " + data.Percentage);
    }

    void Knockback(int enemyAttack)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        // 攻撃された方向（攻撃者 → 自分）
        Vector3 dir = (transform.position - attackHitBox.owner.transform.position).normalized;

        // 吹っ飛び力（スマブラ風）
        float force = enemyAttack * (1 + data.Percentage * 0.05f);

        // 上方向に少し加えるとスマブラっぽくなる
        Vector3 knock = dir * force + Vector3.up * (force * 0.3f);

        rb.AddForce(knock, ForceMode.Impulse);

        Debug.Log("吹っ飛び力: " + force);
    }

}
