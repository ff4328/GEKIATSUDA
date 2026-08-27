
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EffectManager : MonoBehaviour
{


    public CharacterMove chara;

    Vector3 GetPlayerPos;
    Vector3 EffectPos;

    private void Awake()
    {
        chara = FindFirstObjectByType<CharacterMove>();
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
