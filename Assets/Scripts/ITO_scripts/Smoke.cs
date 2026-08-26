using UnityEngine;

public class Smoke : MonoBehaviour
{
    public bool isAttackArea;


    private Smoke_Effect effect;

    private void Awake()
    {
        EffectManager manager =
              FindFirstObjectByType<EffectManager>();

        effect = new Smoke_Effect(manager);
    }

    private void OnTriggerEnter(Collider other)
    {
    
        // 攻撃判定
        if (other.gameObject.tag == "AttackArea")
        {

            isAttackArea = true;

            effect.Smoke(transform.position);

            Destroy(gameObject);

        }
    }
}
