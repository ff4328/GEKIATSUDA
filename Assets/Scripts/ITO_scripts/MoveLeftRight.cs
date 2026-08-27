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
        // 現在位置を保存
        prevPos = transform.position;

        // 左右に移動
        float x = Mathf.Sin(Time.time * speed) * moveWidth;
        transform.position = startPos + new Vector3(x, 0f, 0f);
    }

    public override void HitToCharacter(BaseCharacter hitCharacter)
    {
        // このフレームで足場が移動した量
        Vector3 moveDelta = transform.position - prevPos;
        Debug.Log("aaa");
        // キャラクターも同じだけ移動
        Rigidbody rb = hitCharacter.GetComponent<Rigidbody>();
        rb.MovePosition(hitCharacter.transform.position + moveDelta);
    }
}