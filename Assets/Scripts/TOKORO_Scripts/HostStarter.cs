using UnityEngine;
using Unity.Netcode;

public class HostStarter : MonoBehaviour
{
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        Debug.Log("NGO Host started");
    }
}