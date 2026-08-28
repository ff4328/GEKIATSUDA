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
              GetComponentInParent<EffectManager>();

        if (manager == null)
        {
            Debug.LogError(
                $"{name}: 親にEffectManagerがありません",
                this
            );
            return;
        }
    
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
            if (effect == null)
            {
                Debug.LogWarning($"{name}: TouchGround_Effect がありません");
                return;
            }

            effect.TouchGround(effect.PlayerPos());
        }
        if (other.gameObject.tag == "StageGimmick")
        {
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

    public Vector3 GetFootStepPos()
    {
        Transform transform = this.gameObject.GetComponentInParent<Transform>();
        return transform.position;
    }
    
}
