using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{

    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            CmdMoveSphere(x, z);
        }

        if (isClient)
        {
            if (Input.GetKey(KeyCode.D))
            {
                CmdMoveSphere(1, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                CmdMoveSphere(-1, 0);
            }
            else
            {
                CmdMoveSphere(0, 0);
            }
        }
    }

    [Command]
    void CmdMoveSphere(float x, float z)
    {
         Vector3 v = new Vector3(x, 0, z) * 5f;
        GetComponent<Rigidbody>().AddForce(v);
    }
}