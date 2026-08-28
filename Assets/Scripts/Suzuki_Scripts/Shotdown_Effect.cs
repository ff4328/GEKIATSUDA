using UnityEngine;

public class Shotdown_Effect 
{

    private EffectManager effect;
    private SoundManager sound;
    Vector3 vec;

    public Shotdown_Effect(EffectManager effect)
    {
        this.effect = effect;

    }
    public Shotdown_Effect(SoundManager sound)
    {
        this.sound = sound;

    }

    public Vector3 PlayerPos()
    {
        vec = effect.PlayerEffectPos();
        Debug.Log(vec);
        return vec;
    }

    public void Shotdown(Vector3 pos)
    {
        Debug.Log("Shotdown");

        if (effect == null) return;
        if (effect.Shotdownparticle == null) return;

        ParticleSystem shotdown = UnityEngine.Object.Instantiate(
            effect.Shotdownparticle
        );

        // プレイヤーの子にする
        //shotdown.transform.SetParent(effect.chara.transform, false);

        // プレイヤー基準で0.5下
        //shotdown.transform.localPosition = new Vector3(0, -0.5f, 0);
        //shotdown.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        shotdown.Play();
        sound.audioSource.PlayOneShot(sound.ShotdownClip);

    }
}
