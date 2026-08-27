using UnityEngine;

public class TouchGround : MonoBehaviour
{
    /// <summary>
    /// 接地しているか
    /// </summary>
    public bool isGround;

    /// <summary>
    /// ダブルジャンプできるか
    /// </summary>
    public bool isDoubleJump;


    private TouchGround_Effect effect;
    private void Awake()
    {
        EffectManager manager =
              FindFirstObjectByType<EffectManager>();

        effect = new TouchGround_Effect(manager);



    }

    /// <summary>
    /// 接地判定
    /// otherに入ったColliderのTagがGroundの時処理を行う
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // 接地判定
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
            isDoubleJump = true;
            effect.TouchGround(effect.PlayerPos());
        }
        if (other.gameObject.tag == "StageGimmick")
        {
            if (other.GetComponent<StageGimmickBase>().IsDamageGimmick()) return;
            isGround = true;
            isDoubleJump = true;
            effect.TouchGround(effect.PlayerPos());

        }

    }

    /// <summary>
    /// 離陸判定
    /// Groundから離れたとき処理を行う
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        // 接地判定
        if (other.gameObject.tag == "Ground"|| other.gameObject.tag == "StageGimmick") isGround = false;
        effect.EffectStop();

    }

    public Collider GetFootStepCollider() => this.gameObject.GetComponent<Collider>();
    
}
