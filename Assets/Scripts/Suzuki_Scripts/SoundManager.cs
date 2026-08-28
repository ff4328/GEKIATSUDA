using UnityEngine;

public class SoundManager : MonoBehaviour
{


    public static SoundManager Instance { get; private set; }

    public AudioSource audioSource;

    private void Awake()
    {


        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

       audioSource = GetComponent<AudioSource>();

    }


    [SerializeField] public AudioClip SmokeClip;
    [SerializeField] public AudioClip HealClip;
    [SerializeField] public AudioClip PowerUpClip;
    [SerializeField] public AudioClip InvincibleClip;
    [SerializeField] public AudioClip ShotdownClip;
    [SerializeField] public AudioClip HitClip;
    [SerializeField] public AudioClip ExplosionClip;

}
