using Mirror;
using System.Collections.Generic;
using Utp;
using UnityEngine;

public class GameNetworkManager : RelayNetworkManager
{
    private readonly List<ConnectPlayerNumber> players = new();

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        ConnectPlayerNumber player =
            conn.identity.GetComponent<ConnectPlayerNumber>();

        if (player != null && !players.Contains(player))
        {
            players.Add(player);
            ReassignPlayerNumbers();
        }
    }
    private void ReassignPlayerNumbers()
    {
        Debug.Log($"番号振り直し players.Count = {players.Count}");
        players.RemoveAll(player => player == null);

        for (int i = 0; i < players.Count; i++)
        {
            Debug.Log($"{players[i].name} → {i + 1}P");
            players[i].SetPlayerNumber(i + 1);
        }
    }

    [Server]
    public void SceneMove()
    {
        //ServerChangeScene("CharacterSelectScene");
        ServerChangeScene("Cave");
    }
}