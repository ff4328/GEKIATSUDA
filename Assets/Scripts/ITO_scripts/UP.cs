using Mirror;
using UnityEngine;

public class UP : StageGimmickBase
{
    [SerializeField] private float moveHeight = 2f;
    [SerializeField] private float speed = 2.0f;


    private Vector3 prevPos;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        prevPos = transform.position;
    }

    private void FixedUpdate()
    {
        // 現在位置を保存
        prevPos = transform.position;

        if (isServer)
        {
            MoveLava();
        }
    }

    [Server]
    private void MoveLava()
    {
        // 元の位置 → 上 → 元の位置を繰り返す
        float y = Mathf.PingPong(Time.time * speed, moveHeight);

        transform.position = startPos + new Vector3(0f, y, 0f);
    }

    public override void HitToCharacter(BaseCharacter hitCharacter)
    {
        CharacterMove characterMove =
            hitCharacter.GetComponent<CharacterMove>();

        if (characterMove == null || !characterMove.isLocalPlayer)
            return;

        Vector3 moveDelta =
            transform.position - prevPos;

        Rigidbody rb =
            hitCharacter.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.MovePosition(rb.position + moveDelta);
        }
    }
}
