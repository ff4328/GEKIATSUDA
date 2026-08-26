using System;
using Unity.VisualScripting;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    [SerializeField] private float moveHeight = 2f;
    [SerializeField] private float speed = 2f;

    private Vector3 startPos;

    private Transform player;
    private Vector3 lastPos;

    private Rigidbody playerRb;

    void Start()
    {
        startPos = transform.position;
        lastPos = transform.position;
    }

    void FixedUpdate()
    {
        // 足場を移動
        float y = Mathf.Sin(Time.time * speed) * moveHeight;
        transform.position = startPos + new Vector3(0, y, 0);

        // 足場の移動量
        Vector3 moveDelta = transform.position - lastPos;

        // プレイヤーが乗っていたら一緒に移動
        if (playerRb != null)
        {
            playerRb.MovePosition(playerRb.position + moveDelta);
        }

        lastPos = transform.position;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 上から乗った場合のみ
            if (collision.contacts[0].normal.y > 0.5f)
            {
                playerRb = collision.rigidbody;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.rigidbody == playerRb)
            {
                playerRb = null;
            }
        }
    }

}
