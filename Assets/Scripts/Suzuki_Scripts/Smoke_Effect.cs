using UnityEngine;

public class Smoke_Effect : EffectManager
{

   


    void Start()
    {
       EffectManager Effect = GetComponent<EffectManager>();

        Effect.Smokeparticle.Play();


    }

}
