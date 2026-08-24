using UnityEngine;

public class SwordMan : MonoBehaviour
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
        // 左クリックで攻撃判定を出す
        if (Input.GetMouseButtonDown(0))
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
        data.TakeDamage(enemyAttack);
        Debug.Log("現在のPercentage: " + data.Percentage);
    }
}
