using Mirror;
using UnityEngine;

public class MirrorNetworkManager : NetworkManager
{
    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        Debug.Log("Mirror Server: Client connected");
    }

    public override void OnClientConnect()
    {
        Debug.Log("Mirror Client: Connected to server");
    }
}