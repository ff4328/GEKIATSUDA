using UnityEngine;

public class Barrel : MonoBehaviour{

    public bool isAttackArea;

    private void OnTriggerEnter(Collider other)
    {
        // 接地判定
        if (other.gameObject.tag == "AttackArea")
        {
            isAttackArea = true;


            Destroy(gameObject);
        }
    }
    
    /*
    // 攻撃を受けたとき
    public void TakeDamage(){
        
    }
    */
}
