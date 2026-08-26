using System;
using Unity.VisualScripting;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    [SerializeField] private float moveHeight = 2f;
    [SerializeField] private float speed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float y =Mathf.Sin(Time.time * speed) * moveHeight;
        transform.position = startPos + new Vector3(0, y, 0);
    }
}
