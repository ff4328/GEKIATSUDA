using UnityEngine;

public class Barrier : MonoBehaviour
{
    public BaseCharacter player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AttackArea")
        {
            player.StartInvincible(5f); // ★5秒無敵
            Destroy(gameObject);        // ★アイテムを消す
        }
    }
}


