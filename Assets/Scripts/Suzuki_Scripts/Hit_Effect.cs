using UnityEngine;

public class Hit_Effect
{
    private EffectManager effect;
    private SoundManager sound;
    Vector3 vec;

    public Hit_Effect(EffectManager effect)
    {
        this.effect = effect;

    }
    public Hit_Effect(SoundManager sound)
    {
        this.sound = sound;

    }

    public Vector3 PlayerPos()
    {
        vec = effect.PlayerEffectPos();
        Debug.Log(vec);
        return vec;
    }

    public void Hit(Vector3 pos)
    {
        Debug.Log("Grounded");

        if (effect == null) return;
        if (effect.Hitparticle == null) return;

        ParticleSystem hit = UnityEngine.Object.Instantiate(
            effect.Hitparticle
        );

        // プレイヤーの子にする
        hit.transform.SetParent(effect.chara.transform, false);

        // エフェクト微調整
        hit.transform.localPosition = new Vector3(0, -0.5f, 0);
        hit.transform.localScale = new Vector3(0.3f, 0.2f, 0.3f);
        ParticleSystem.MainModule main = hit.main;
        main.simulationSpeed = 6.0f;

        hit.Play();
        sound.audioSource.PlayOneShot(sound.HitClip);

        UnityEngine.Object.Destroy(hit.gameObject, 0.5f);
    }
}
