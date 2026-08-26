using UnityEngine;

public class Atuage : MonoBehaviour
{
    public bool isAttackArea;

    public CharaDataBase charaData;

    private Heal_Effect effect;

    private void Awake()
    {
        EffectManager manager =
              FindFirstObjectByType<EffectManager>();

        effect = new Heal_Effect(manager);


    }

    void Start()
    {
        charaData = new CharaDataBase();
    }
    private void OnTriggerEnter(Collider other)
    {
        // 攻撃判定
        if (other.gameObject.tag == "AttackArea")
        {
            isAttackArea = true;

            charaData.Heal(100);
            Debug.Log("回復");

            effect.Heal(transform.position);
            Destroy(gameObject);
        }
    }
}
