using Mirror;
using UnityEngine;

public class MoveLeftRight : StageGimmickBase
{
    [SerializeField] private float moveWidth = 2f;
    [SerializeField] private float speed = 2f;

    private Vector3 prevPos;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        prevPos = transform.position;
    }

    private void FixedUpdate()
    {
        // 全端末で更新する
        prevPos = transform.position;

        // Serverだけ床を動かす
        if (isServer)
        {
            MoveGround();
        }
    }

    [Server]
    private void MoveGround()
    {
        float x = Mathf.Sin(Time.time * speed) * moveWidth;
        transform.position = startPos + new Vector3(x, 0f, 0f);
    }

    public override void HitToCharacter(BaseCharacter hitCharacter)
    {
        CharacterMove characterMove =
            hitCharacter.GetComponent<CharacterMove>();

        // このPCが操作しているPlayerだけ追従させる
        if (characterMove == null || !characterMove.isLocalPlayer)
            return;

        Vector3 moveDelta = transform.position - prevPos;

        Rigidbody rb = hitCharacter.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.MovePosition(rb.position + moveDelta);
        }
    }
}