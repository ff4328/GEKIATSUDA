using UnityEngine;

public class Smoke : MonoBehaviour
{
    public bool isAttackArea;


    Smoke_Effect effect;

    private void OnTriggerEnter(Collider other)
    {
        Smoke_Effect effect = GetComponent<Smoke_Effect>();
        // 攻撃判定
        if (other.gameObject.tag == "AttackArea")
        {
            isAttackArea = true;


            Destroy(gameObject);

            effect.Smoke();
        }
    }
}
