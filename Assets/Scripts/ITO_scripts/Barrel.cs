using UnityEngine;

public class Barrel : MonoBehaviour{
    private void OnTriggerEnter(Collider other)
    {
        // 攻撃判定
        if (other.gameObject.tag == "AttackArea")
        {
            Destroy(gameObject);
        }
    }
}
