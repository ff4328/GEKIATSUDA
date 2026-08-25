using UnityEngine;

public class Smoke : MonoBehaviour
{
    public bool isAttackArea;

    private void OnTriggerEnter(Collider other)
    {
        // 攻撃判定
        if (other.gameObject.tag == "AttackArea")
        {
            isAttackArea = true;


            Destroy(gameObject);
        }
    }
}
