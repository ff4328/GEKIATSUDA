using Unity.VisualScripting;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    /// <summary>
    /// 使用するクラス
    /// </summary>
    private CharacterMove _characterMove;
    
    [SerializeField]
    [Tooltip("剣を持っている手")]
    private Transform HandOfHoldingSword;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _characterMove = GetComponent<CharacterMove>();
        if( _characterMove == null )_characterMove = this.AddComponent<CharacterMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
