using Unity.Netcode;
using UnityEngine;

public class GunMan : BaseCharacter
{
    public bool isStunned = false;
    public bool isKnockback = false;

    private Rigidbody rb;

    private void Awake()
    {
        attackOffset = new Vector3(1, 1, 0);
        strongAttackOffset = new Vector3(2, 1, 0);
    }
    protected override void Start()
    {
        // ★職業ごとの最低値
        baseAttack = 2;
        baseSpeed = 1;
        baseSize = 1;

        // ★BaseCharacter の初期化（data, characterMove）
        base.Start();


        // ★SwordMan 固有の初期化
        rb = GetComponent<Rigidbody>();
    }

    protected override void Update()
    {
        base.Update();
        if (isStunned || isKnockback) return;
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
