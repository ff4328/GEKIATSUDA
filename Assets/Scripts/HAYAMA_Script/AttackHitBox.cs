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
        Debug.Log("HitBox 状態: " + active);

        if (active)
        {
            Debug.Log("HitBox 位置: " + transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyDummy enemy = other.GetComponent<EnemyDummy>();
        if (enemy != null)
        {
            enemy.OnHit(owner.data.Attack, owner.transform.position);
        }
    }
}
