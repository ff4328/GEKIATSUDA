
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }

    public SoundManager sound;


    public CharacterMove chara;

    Vector3 GetPlayerPos;
    Vector3 EffectPos;

    private void Awake()
    {


        sound = FindFirstObjectByType<SoundManager>();
        chara = FindFirstObjectByType<CharacterMove>();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);


    }

    [SerializeField] public ParticleSystem Smokeparticle;
    [SerializeField] public ParticleSystem Healparticle;
    [SerializeField] public ParticleSystem TouchGuroundparticle;
    [SerializeField] public ParticleSystem Shotdownparticle;
    [SerializeField] public ParticleSystem Hitparticle;
    [SerializeField] public ParticleSystem ExplosionparticleFirst;
    [SerializeField] public ParticleSystem ExplosionparticleEnd;

    public void Update()
    {
        EffectPos = chara.GetPos();
    }

    public Vector3 PlayerEffectPos()
    {
       GetPlayerPos = EffectPos;

       return GetPlayerPos;
    }
}
