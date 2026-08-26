using System;
using Unity.VisualScripting;
using UnityEngine;

public class MoveLeftRight : MonoBehaviour
{
    [SerializeField] private float moveWidth = 2f;
    [SerializeField] private float speed = 2f;

    private Vector3 startPos;

    private Transform player;
    private Vector3 lastPos;

    void Start()
    {
        startPos = transform.position;
        lastPos = transform.position;
    }

    void Update()
    {
        float x = Mathf.Sin(Time.time * speed) * moveWidth;
        transform.position = startPos + new Vector3(x, 0, 0);

        if (player != null)
        {
            Vector3 moveDelta = transform.position - lastPos;
            player.position += moveDelta;
        }

        lastPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 上から乗った場合だけ
            if (collision.contacts[0].normal.y > 0.5f)
            {
                player = collision.transform;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player == collision.transform)
            {
                player = null;
            }
        }
    }
}
