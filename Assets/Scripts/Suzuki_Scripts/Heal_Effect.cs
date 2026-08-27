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

        ParticleSystem heal = UnityEngine.Object.Instantiate(
            effect.Healparticle
        );

        // プレイヤーの子にする
        heal.transform.SetParent(effect.chara.transform, false);

        // プレイヤー基準で0.5下
        heal.transform.localPosition = new Vector3(0, -0.5f, 0);
        heal.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        heal.Play();

        UnityEngine.Object.Destroy(heal.gameObject, 0.5f);
    }
}