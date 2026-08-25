using System;
using UnityEngine;

public class Heal_Effect : EffectManager
{




    void Start()
    {
        EffectManager Effect = GetComponent<EffectManager>();

        Effect.Healparticle.Play();


    }

}
