using Mirror;
using System.Collections.Generic;
using Utp;
using UnityEngine;

public class GameNetworkManager : RelayNetworkManager
{
    private readonly List<ConnectPlayerNumber> players = new();

    private List<GameObject> _characterPrefabs;

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
        ServerChangeScene("CharacterSelectScene");
        //ServerChangeScene("Cave");
    }

    [Server]
    public void CheckAllReady()
    {
        if (players.Count == 0)
            return;

        foreach (ConnectPlayerNumber player in players)
        {
            CharacterSelectPlayer selectPlayer =
                player.GetComponent<CharacterSelectPlayer>();

            if (selectPlayer == null)
            {
                Debug.LogWarning("CharacterSelectPlayerがありません");
                return;
            }

            if (!selectPlayer.IsReady)
            {
                Debug.Log($"{player.PlayerNumber}P はまだReadyしていません");
                return;
            }
        }

        Debug.Log("全員Ready！");
        ServerChangeScene("Cave");
    }

    [Server]
    private void SpawnBattlePlayers()
    {
        // コピーを作る
        // ReplacePlayerForConnection中にplayersを直接触るのを避ける
        var currentPlayers = new List<ConnectPlayerNumber>(players);

        foreach (ConnectPlayerNumber lobbyPlayer in currentPlayers)
        {
            CharacterSelectPlayer select =
                lobbyPlayer.GetComponent<CharacterSelectPlayer>();

            if (select == null)
            {
                Debug.LogError("CharacterSelectPlayerがありません");
                continue;
            }

            int characterID = select.CharacterID;

            // 不正なID対策
            if (characterID < 0 || characterID >= _characterPrefabs.Count)
            {
                Debug.LogError($"不正なCharacterID: {characterID}");
                continue;
            }

            // このLobbyPlayerを所有している接続
            NetworkConnectionToClient conn =
                lobbyPlayer.connectionToClient;

            if (conn == null)
            {
                Debug.LogError("connectionToClientがありません");
                continue;
            }

            // 選択された戦闘キャラを生成
            GameObject newPlayer =
                Instantiate(_characterPrefabs[characterID]);

            // P番号を引き継ぐ
            ConnectPlayerNumber newPlayerNumber =
                newPlayer.GetComponent<ConnectPlayerNumber>();

            if (newPlayerNumber != null)
            {
                newPlayerNumber.SetPlayerNumber(lobbyPlayer.PlayerNumber);
            }

            // LobbyPlayer → BattlePlayer
            NetworkServer.ReplacePlayerForConnection(
                conn,
                newPlayer,
                ReplacePlayerOptions.KeepAuthority
            );
        }
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        base.OnServerSceneChanged(sceneName);

        if (sceneName == "Cave")
        {
            SpawnBattlePlayers();
        }
    }
}