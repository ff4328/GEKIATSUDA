using UnityEngine;
using Unity.Netcode;

public class NetworkSetup : MonoBehaviour
{
    void Awake()
    {
        NetworkManager.Singleton.NetworkConfig = new NetworkConfig();
    }
}