using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    private void FixedUpdate()
    {
        Debug.Log($"FixedUpdate {netId} / Local:{isLocalPlayer}");
        if (isLocalPlayer)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            CmdMoveSphere(x, z);
        }
    }

    [Command]
    void CmdMoveSphere(float x, float z)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        Vector3 v = new Vector3(x, 0, z) * 5f;

        Debug.Log($"Before: {rb.linearVelocity.ToString("F6")}");
        rb.AddForce(v);
        Debug.Log($"After: {rb.linearVelocity.ToString("F6")}");
    }
}