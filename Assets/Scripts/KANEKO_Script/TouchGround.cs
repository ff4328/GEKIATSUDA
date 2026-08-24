using UnityEngine;

public class TouchGround : MonoBehaviour
{
    public bool isGround;
    public bool isDoubleJump;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    // 接地判定
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        isGround = true;
    //        isDoubleJump = true;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        // 接地判定
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
            isDoubleJump = true;
        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    // 接地判定
    //    if (collision.gameObject.tag == "Ground") isGround = false;
    //}

    private void OnTriggerExit(Collider other)
    {
        // 接地判定
        if (other.gameObject.tag == "Ground") isGround = false;
    }

}
