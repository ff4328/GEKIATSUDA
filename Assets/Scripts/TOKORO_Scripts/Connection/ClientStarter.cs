using TMPro;
using UnityEngine;
using Utp;

public class ClientStarter : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] RelayNetworkManager relay;
    [SerializeField] private GameObject cutting;
    [SerializeField] private GameObject host;
    [SerializeField] private GameObject client;
    [SerializeField] private GameObject input;

    public void StartClient()
    {
        if (inputField.text == "")
        {
            Debug.Log("Join code must be 6 characters long.");
            return;
        }

        relay.relayJoinCode = inputField.text;

        relay.JoinRelayServer();

        cutting.SetActive(true);
        host.SetActive(false);
        client.SetActive(false);
        input.SetActive(false);
    }
}