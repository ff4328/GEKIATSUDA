using Mirror;
using UnityEngine;

public class MirrorStarter : MonoBehaviour
{
    public void StartMirrorHost()
    {
        NetworkManager.singleton.StartHost();
    }

    public void StartMirrorClient(string ip)
    {
        NetworkManager.singleton.networkAddress = ip;
        NetworkManager.singleton.StartClient();
    }
}