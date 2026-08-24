using UnityEngine;

public class Barrel : MonoBehaviour{
    // 攻撃を受けたとき
    public void TakeDamage(){
        Destroy(gameObject);
    }
}
