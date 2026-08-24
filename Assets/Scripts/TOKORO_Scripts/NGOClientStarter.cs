using UnityEngine;
using Unity.Netcode;

public class NGOClientStarter : MonoBehaviour
{
    public void StartNGOClient()
    {
        NetworkManager.Singleton.StartClient();
    }
}