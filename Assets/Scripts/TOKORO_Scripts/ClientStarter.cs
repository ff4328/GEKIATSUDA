using UnityEngine;
using Unity.Netcode;

public class ClientStarter : MonoBehaviour
{
    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        Debug.Log("NGO Client started");
    }
}
