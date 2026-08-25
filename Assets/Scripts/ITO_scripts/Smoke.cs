using UnityEngine;

public class Smoke : MonoBehaviour
{
    public bool isAttackArea;


    private Smoke_Effect effect;

    private void Awake()
    {
        effect = FindFirstObjectByType<Smoke_Effect>();
    }

    private void OnTriggerEnter(Collider other)
    {
    
        // 攻撃判定
        if (other.gameObject.tag == "AttackArea")
        {

           

            isAttackArea = true;

            effect.Smoke();

            Destroy(gameObject);

        }
    }
}
