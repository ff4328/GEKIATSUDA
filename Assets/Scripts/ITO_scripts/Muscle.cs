using UnityEditor.Rendering;
using UnityEngine;

public class Muscle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 攻撃判定
        if (other.gameObject.tag == "AttackArea")
        {
            Destroy(gameObject);
        }
    }
}
