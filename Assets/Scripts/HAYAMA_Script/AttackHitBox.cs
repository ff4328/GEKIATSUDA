using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    public SwordMan owner;
    private Collider col;

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
    }
}
