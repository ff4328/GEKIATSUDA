using UnityEngine;
using Utp;

public class DisconnectButton : MonoBehaviour
{
    [SerializeField] private RelayNetworkManager relay;

    public void Disconnect()
    {
        if (relay == null)
            return;

        if (relay.mode == Mirror.NetworkManagerMode.Host)
        {
            relay.StopHost();
        }
        else if (relay.mode == Mirror.NetworkManagerMode.ClientOnly)
        {
            relay.StopClient();
        }
    }
}