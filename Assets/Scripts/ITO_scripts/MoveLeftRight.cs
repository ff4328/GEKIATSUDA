using System;
using Unity.VisualScripting;
using UnityEngine;

public class MoveLeftRight : MonoBehaviour
{
    [SerializeField] private float moveWidth = 2f;
    [SerializeField] private float speed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float x = Mathf.Sin(Time.time * speed) * moveWidth;
        transform.position = startPos + new Vector3(x, 0, 0);
    }
}
