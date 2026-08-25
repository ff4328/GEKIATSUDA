using TMPro;
using UnityEngine;
using Utp;

public class HostStarter : MonoBehaviour
{
    [SerializeField] RelayNetworkManager relay;
    [SerializeField] TMP_Text roomID;
    [SerializeField] static string roomName;
    [SerializeField] private GameObject client;
    [SerializeField] private GameObject cutting;
    [SerializeField] private GameObject host;
    [SerializeField] private GameObject input;

    public void StartHost()
    {
        relay.StartRelayHost(2);

        cutting.SetActive(true);
        client.SetActive(false);
        host.SetActive(false);
        input.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (relay.GetRoomID())
            roomID.text = $"Room ID: {relay.relayJoinCode}";
    }
}