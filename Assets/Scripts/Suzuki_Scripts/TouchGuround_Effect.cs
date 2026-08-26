using UnityEngine;

public class TouchGuround_Effect
{

    private EffectManager effect;

    public TouchGuround_Effect(EffectManager effect)
    {
        this.effect = effect;
        
    }

    public void TouchGuround(Vector3 pos)
    {


        if (effect == null) return;


        if (effect.TouchGuroundparticle == null) return;



        Vector3 spawnPos = new Vector3(
            pos.x,
            pos.y,
            pos.z
        );

        ParticleSystem touchGuround = UnityEngine.Object.Instantiate(
            effect.TouchGuroundparticle,
            spawnPos,
            Quaternion.identity
        );
        touchGuround.Play();
    }
}