using UnityEngine;

public class Smoke_Effect
{
    private EffectManager effect;

    public Smoke_Effect(EffectManager effect)
    {
        this.effect = effect;

        Debug.Log("EffectManager取得成功: " + effect.gameObject.name);
    }

    public void Smoke(Vector3 pos)
    {
        Debug.Log("Smoke()開始");

        if (effect == null)
        {
            Debug.LogError("EffectManagerがNULLです");
            return;
        }

        if (effect.Smokeparticle == null)
        {
            Debug.LogError("SmokeparticleがNULLです！");
            return;
        }

        Debug.Log("Smokeparticle取得成功: " + effect.Smokeparticle.name);

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

        Debug.Log("生成位置: " + smoke.transform.position);
        Debug.Log("Smoke生成成功: " + smoke.name);

        smoke.Play();

        Debug.Log("Smoke再生中: " + smoke.isPlaying);
    }
}