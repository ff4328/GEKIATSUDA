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
        Debug.Log("Trigger 入った: " + other.name);

        SwordMan enemy = other.GetComponent<SwordMan>();
        if (enemy != null)
        {
            Debug.Log("当たった！攻撃力: " + owner.data.GetAttack());
            enemy.OnHit(owner.data.GetAttack());
        }
        else
        {
            Debug.Log("SwordMan が見つからない → 敵に SwordMan.cs が付いてない可能性");
        }
    }
}
