
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


    public void Update()
    {
     
        EffectPos = chara.GetPos();
        Debug.Log(EffectPos);
    }

    public Vector3 PlayerEffectPos()
    {
       GetPlayerPos = EffectPos;

       return GetPlayerPos;
    }
}
