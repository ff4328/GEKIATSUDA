using System;
using Unity.VisualScripting;
using UnityEngine;

public class Heal_Effect 
{

    private EffectManager effect;
    Vector3 vec;
    public Heal_Effect(EffectManager effect)
    {
        this.effect = effect;

    }

    public Vector3 PlayerPos()
    {
        vec = effect.PlayerEffectPos();
        Debug.Log(vec);
        return vec;
    }

    public void Heal(Vector3 pos)
    {
        Debug.Log("heal");

        if (effect == null) return;


        if (effect.Healparticle == null) return;



        Vector3 spawnPos = new Vector3(
            pos.x,
            pos.y,
            pos.z
        );

        ParticleSystem heal = UnityEngine.Object.Instantiate(
            effect.Healparticle,
            effect.PlayerEffectPos(),
            Quaternion.identity
        );

        heal.transform.SetParent(effect.chara.transform);

        heal.Play();

        UnityEngine.Object.Destroy(heal.gameObject, 0.5f);
    }

}