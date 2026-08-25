using TMPro;
using UnityEngine;

public class FollowText : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset;

    private void Update()
    {
        transform.position =
            Camera.main.WorldToScreenPoint(target.position + offset);
    }
}