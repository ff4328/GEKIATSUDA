using UnityEngine;

public class CameraMove : MonoBehaviour
{

    Camera MainCamera;

    BaseCharacter[] characterDatas;
    CharacterMove[] characterPositionss;


    private void Awake()
    {
        MainCamera = GetComponent<Camera>();

        characterDatas = FindObjectsByType<BaseCharacter>(FindObjectsSortMode.None);




    }









}
