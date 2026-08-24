using UnityEngine;

public class SwordMan : MonoBehaviour
{
    public CharaDataBase data;

    // 攻撃判定用コライダー（手や体に付ける）
    [SerializeField] private Collider attackHitBox;

    // 攻撃中だけ判定を有効化
    private bool isAttacking = false;

    void Start()
    {
        data = new CharaDataBase();
        SetUp();

        // 最初は攻撃判定を無効化
        attackHitBox.enabled = false;
    }

    void SetUp()
    {
        data.SetPercentage(0);
        data.SetAttack(3);
        data.SetSpeed(1);
        data.SetSize(1);
        data.SetJumpPower(2);
    }

    void Update()
    {
        // 攻撃ボタン（例：スペースキー）
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartAttack();
        }
    }

    // 攻撃開始
    void StartAttack()
    {
        isAttacking = true;
        attackHitBox.enabled = true;

        // 攻撃判定は0.2秒だけ有効にする（アニメの攻撃フレーム想定）
        Invoke(nameof(EndAttack), 0.2f);
    }

    // 攻撃終了
    void EndAttack()
    {
        isAttacking = false;
        attackHitBox.enabled = false;
    }

    // 攻撃判定が相手に当たった時
    private void OnTriggerEnter(Collider other)
    {
        if (!isAttacking) return;

        SwordMan enemy = other.GetComponent<SwordMan>();
        if (enemy != null)
        {
            enemy.OnHit(data.GetAttack());
        }
    }

    // ダメージ処理
    public void OnHit(int enemyAttack)
    {
        data.TakeDamage(enemyAttack);

        Debug.Log($"{name} が攻撃を受けた！ 現在のPercentage: {data.Percentage}");
    }
}
