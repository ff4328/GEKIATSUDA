using System;
using UnityEngine;

public class Heal_Effect 
{

    private EffectManager effect;

    public Heal_Effect(EffectManager effect)
    {
        this.effect = effect;

    }

    public void Heal(Vector3 pos)
    {


        if (effect == null) return;


        if (effect.Smokeparticle == null) return;



        Vector3 spawnPos = new Vector3(
            pos.x,
            pos.y,
            pos.z
        );

        ParticleSystem heal = UnityEngine.Object.Instantiate(
            effect.Healparticle,
            spawnPos,
            Quaternion.identity
        );
        heal.Play();
    }
}