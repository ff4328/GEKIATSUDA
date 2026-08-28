using System.Threading.Tasks;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Explosion_Effect effect;

    private void Awake()
    {

        EffectManager manager =
              FindFirstObjectByType<EffectManager>();

        effect = new Explosion_Effect(manager);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 攻撃判定
        if (other.gameObject.tag == "AttackArea")
        {
            effect.Explosion(transform.position);

          
            Destroy(gameObject);
        }
    }

}
