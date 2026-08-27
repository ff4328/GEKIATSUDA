using Unity.Mathematics;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    private Collider col;
    public bool isPowerUp = false;
    public BaseCharacter character;
    float count = 0;

    void Awake()
    {
        col = GetComponent<Collider>();
    }

    public void SetActiveHitBox(bool active)
    {
        col.enabled = active;
    }

    public int attackPower;

    public void SetAttackPower(int power)
    {
        attackPower = power;
    }

    private void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponent<BaseCharacter>();
        if (target != null)
        {
            target.OnHit(attackPower, transform.position);
        }


        if (other.gameObject.tag == "Muscle")
        {
            isPowerUp = true;
            TemporaryPowerUp(20);
            Debug.Log("パワーアップ");
        }

        if (other.gameObject.tag == "Barrier")
        {
            character.StartInvincible(5f);
            Debug.Log("無敵");
        }

    }

    public void TemporaryPowerUp(int power)
    {
        character.finalAttackPower += power;
    }
    public void TemporaryPowerDown(int power)
    {
        character.finalAttackPower -= power;
    }
}
