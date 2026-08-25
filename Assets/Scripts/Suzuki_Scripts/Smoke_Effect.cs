using UnityEngine;

public class Smoke_Effect : EffectManager
{

    public void Start()
    {
       EffectManager Effect = GetComponent<EffectManager>();

        Effect.Smokeparticle.Play();


    }

}
