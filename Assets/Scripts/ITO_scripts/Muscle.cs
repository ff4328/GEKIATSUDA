using UnityEditor.Rendering;
using UnityEngine;

public class Muscle : MonoBehaviour
{
    public bool isAttackArea;

    public BaseCharacter character;

    void Start()
    {
        character = new BaseCharacter();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 攻撃判定
        if (other.gameObject.tag == "AttackArea")
        {
            isAttackArea = true;
            character.TemporaryPowerUp(20);
            Debug.Log("パワーアップ");
            Destroy(gameObject);
        }
    }
}
