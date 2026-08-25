using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public RankParameter rank;

    public int baseAttack = 3;
    public float baseSpeed = 1;
    public int baseSize = 1;

    public CharaDataBase data;
    public CharacterMove characterMove;

    public int finalAttackPower;

    protected virtual void Start()
    {
        data = new CharaDataBase();

        finalAttackPower = rank.GetAttack(baseAttack);

        float finalSpeed = rank.GetSpeed(baseSpeed);
        int finalSize = rank.GetSize(baseSize);

        characterMove.moveSpeed = finalSpeed * 0.1f;
        transform.localScale = Vector3.one * finalSize;
    }

    // ★★★ これを追加する ★★★
    public virtual void OnHit(int enemyAttack, Vector3 attackerPos)
    {
    }
    public virtual void OnEnvironmentDamage(int damage)
    {
        data.TakeDamage(damage);
    }

}
