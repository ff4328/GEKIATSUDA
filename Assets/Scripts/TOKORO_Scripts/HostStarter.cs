using TMPro;
using UnityEngine;
using Utp;

public class HostStarter : MonoBehaviour
{
    [SerializeField] RelayNetworkManager relay;
    [SerializeField] TMP_Text roomID;

    public void StartHost()
    {
        relay.StartRelayHost(2);
    }

    private void FixedUpdate()
    {
        roomID.text = $"Room ID: {relay.GetRoomID()}";
    }
}