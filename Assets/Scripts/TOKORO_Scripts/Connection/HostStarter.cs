using TMPro;
using UnityEngine;
using Utp;

public class HostStarter : MonoBehaviour
{
    [SerializeField] RelayNetworkManager relay;
    [SerializeField] TMP_Text roomID;
    [SerializeField] static string roomName;

    public void StartHost()
    {
        relay.StartRelayHost(2);
    }

    private void FixedUpdate()
    {
        if (relay.GetRoomID())
            roomID.text = $"Room ID: {relay.relayJoinCode}";
    }
}