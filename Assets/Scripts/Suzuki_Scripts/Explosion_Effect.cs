using System.Threading.Tasks;
using UnityEngine;

public class Explosion_Effect
{
    private EffectManager effect;

    public Explosion_Effect(EffectManager effect)
    {
        this.effect = effect;

    }

    public async Task Explosion(Vector3 pos)
    {


        if (effect == null) return;


        if (effect.ExplosionparticleFirst == null) return;
        if (effect.ExplosionparticleEnd == null) return;



        Vector3 spawnPos = new Vector3(
            pos.x,
            pos.y,
            pos.z
        );

        //再生時間
        ParticleSystem.MainModule firstEffect = effect.ExplosionparticleFirst.main;

        float durationFirst = firstEffect.duration;
        float lifetimeFirst = firstEffect.startLifetime.constantMax;

        float totalFirstTime = durationFirst + lifetimeFirst;


        ParticleSystem.MainModule endEffect = effect.ExplosionparticleEnd.main;

        float durationEnd = endEffect.duration;
        float lifetimeEnd = endEffect.startLifetime.constantMax;

        float totalEndTime = durationEnd + lifetimeEnd;


        ParticleSystem explosionFirst = Object.Instantiate(
            effect.ExplosionparticleFirst,
            spawnPos,
            Quaternion.identity
        );

        UnityEngine.Object.Destroy(explosionFirst.gameObject, totalFirstTime);
        explosionFirst.Play();

        //1つ目の再生間ってから２個目の再生

        await Task.Delay((int)(totalFirstTime * 1000));

        if (effect == null) return;


        ParticleSystem explosionEnd = Object.Instantiate(
            effect.ExplosionparticleEnd,
            spawnPos,
            Quaternion.identity
        );

        UnityEngine.Object.Destroy(explosionEnd.gameObject, totalEndTime);

        explosionEnd.Play();

        await Task.Delay((int)(totalEndTime * 1000));



          
        
    }



}
