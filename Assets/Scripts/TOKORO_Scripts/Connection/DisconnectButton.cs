using TMPro;
using UnityEngine;
using Utp;

public class DisconnectButton : MonoBehaviour
{
    [SerializeField] private RelayNetworkManager relay;
    [SerializeField] TMP_Text roomID;
    [SerializeField] private GameObject client;
    [SerializeField] private GameObject host;
    [SerializeField] private GameObject cutting;
     [SerializeField] private GameObject start;
    [SerializeField] private GameObject input;

    public void Disconnect()
    {
        if (relay == null)
            return;

        if (relay.mode == Mirror.NetworkManagerMode.Host)
        {
            relay.StopHost();
            roomID.text = $"Room ID:";

            host.SetActive(true);
            client.SetActive(true);
            input.SetActive(true);
            start.SetActive(false);
            cutting.SetActive(false);
        }
        else if (relay.mode == Mirror.NetworkManagerMode.ClientOnly)
        {
            relay.StopClient();
            roomID.text = $"Room ID:";

            host.SetActive(true);
            client.SetActive(true);
            input.SetActive(true);
            start.SetActive(false);
            cutting.SetActive(false);
        }
    }
}