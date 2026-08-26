using TMPro;
using UnityEngine;

public class FollowText : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}