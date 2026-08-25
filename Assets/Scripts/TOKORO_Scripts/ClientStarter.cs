using TMPro;
using UnityEngine;
using Utp;

public class ClientStarter : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] RelayNetworkManager relay;

    public void StartClient()
    {
        relay.relayJoinCode = inputField.text;

        relay.JoinRelayServer();
    }
}