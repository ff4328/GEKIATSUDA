using UnityEngine;

public class Smoke_Effect
{
    private EffectManager effect;

    public Smoke_Effect(EffectManager effect)
    {
        this.effect = effect;

    }

    public void Smoke(Vector3 pos)
    {
       

        if (effect == null)return;
       

        if (effect.Smokeparticle == null) return;
       


        Vector3 spawnPos = new Vector3(
            pos.x,
            pos.y,
            pos.z
        );

        ParticleSystem smoke = Object.Instantiate(
            effect.Smokeparticle,
            spawnPos,
            Quaternion.identity
        );
        smoke.Play();
    }
}