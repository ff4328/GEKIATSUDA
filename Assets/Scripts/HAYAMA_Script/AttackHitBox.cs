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

    private void OnTriggerEnter(Collider other)
    {
        SwordMan enemy = other.GetComponent<SwordMan>();
        if (enemy != null)
        {
            // ★攻撃者の位置を正しく渡す
            enemy.OnHit(owner.data.Attack, owner.transform.position);
        }
    }
}
