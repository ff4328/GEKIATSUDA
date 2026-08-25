using UnityEngine;

public class Smoke_Effect : EffectManager
{


    public void Smoke()
    {


       EffectManager Effect = GetComponent<EffectManager>();
       Effect.Smokeparticle.Play();
    }

}
