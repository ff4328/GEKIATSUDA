using TMPro;
using UnityEngine;
using Utp;

public class DisconnectButton : MonoBehaviour
{
    [SerializeField] private RelayNetworkManager relay;
    [SerializeField] TMP_Text roomID;

    public void Disconnect()
    {
        if (relay == null)
            return;

        if (relay.mode == Mirror.NetworkManagerMode.Host)
        {
            relay.StopHost();
            roomID.text = $"Room ID:";
        }
        else if (relay.mode == Mirror.NetworkManagerMode.ClientOnly)
        {
            relay.StopClient();
            roomID.text = $"Room ID:";
        }
    }
}