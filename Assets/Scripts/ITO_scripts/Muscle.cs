
using UnityEngine;

public class Muscle : MonoBehaviour
{

    private SoundManager sound;
    
    private Muscle (SoundManager sound)
    {

        this.sound = sound;

    }

    private void OnTriggerEnter(Collider other)
    {
        // 攻撃判定
        if (other.gameObject.tag == "AttackArea")
        {
            Destroy(gameObject);
            sound.audioSource.PlayOneShot(sound.PowerUpClip);
        }
    }
}
