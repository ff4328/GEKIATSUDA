using UnityEngine;

public class Smoke_Effect : MonoBehaviour
{
    private EffectManager effect;

    private void Awake()
    {
        effect = FindFirstObjectByType<EffectManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Smoke();
        }
    }

    public void Smoke()
    {
        if (effect == null)
        {
            Debug.LogError("EffectManagerが見つかりません");
            return;
        }

        if (effect.Smokeparticle == null)
        {
            Debug.LogError("SmokeParticleが設定されていません");
            return;
        }

        ParticleSystem smoke = Instantiate(
            effect.Smokeparticle,
            transform.position,
            Quaternion.identity
        );

        smoke.Play();

        Destroy(smoke.gameObject, smoke.main.duration + 1f);
    }
}