using UnityEngine;
using Unity.Netcode;

public class NGOHostStarter : MonoBehaviour
{
    public void StartNGOHost()
    {
        NetworkManager.Singleton.StartHost();
    }
}