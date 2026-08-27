using System;
using Unity.VisualScripting;
using UnityEngine;

public class TouchGround_Effect
{

    private EffectManager effect;
    Vector3 vec;

    public TouchGround_Effect(EffectManager effect)
    {
        this.effect = effect;

    }

    public Vector3 PlayerPos()
    {
        if (effect == null)
        {
            Debug.LogError("TouchGround_Effect: EffectManager が null");
            return Vector3.zero;
        }

        vec = effect.PlayerEffectPos();
        return vec;
    }

    public void TouchGround(Vector3 pos)
    {

        if (effect == null) return;
        if (effect.TouchGuroundparticle == null) return;

        ParticleSystem touchGuround = UnityEngine.Object.Instantiate(
            effect.TouchGuroundparticle
        );

        // プレイヤーの子にする
        touchGuround.transform.SetParent(effect.chara.transform, false);

        // エフェクト微調整
        //touchGuround.transform.localPosition = new Vector3(0, -0.5f, 0);
        touchGuround.transform.localScale = new Vector3(0.3f, 0.2f, 0.3f);
        ParticleSystem.MainModule main = touchGuround.main;
        main.simulationSpeed = 6.0f;

        touchGuround.Play();

        UnityEngine.Object.Destroy(touchGuround.gameObject, 0.5f);
    }

    public void EffectStop()
    {
        ParticleSystem touchGuround = UnityEngine.Object.Instantiate(
          effect.TouchGuroundparticle
      );

        touchGuround.Stop();
    }
}