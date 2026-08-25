using System;
using UnityEngine;

public class Heal_Effect : EffectManager
{




    void Heal()
    {
        EffectManager Effect = GetComponent<EffectManager>();

        Effect.Healparticle.Play();


    }

}
