using Mirror;
using System.Collections.Generic;
using Utp;
using UnityEngine;

public class GameNetworkManager : RelayNetworkManager
{
    private class BattlePlayerData
    {
        public NetworkConnectionToClient connection;
        public int playerNumber;
        public int characterID;
    }

    private readonly List<BattlePlayerData> _battlePlayerData = new();

    private readonly List<ConnectPlayerNumber> players = new();

    [SerializeField] private List<GameObject> _characterPrefabs=new();

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
        players.RemoveAll(player => player == null);

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

        // ★ シーン変更前にデータを保存
        _battlePlayerData.Clear();

        foreach (ConnectPlayerNumber player in players)
        {
            CharacterSelectPlayer select =
                player.GetComponent<CharacterSelectPlayer>();

            _battlePlayerData.Add(new BattlePlayerData
            {
                connection = player.connectionToClient,
                playerNumber = player.PlayerNumber,
                characterID = select.CharacterID
            });
        }

        ServerChangeScene("Cave");
    }

    [Server]
    private void SpawnBattlePlayers()
    {
        BattleSpawnPoints spawnPoints =
            FindFirstObjectByType<BattleSpawnPoints>();

        if (spawnPoints == null)
        {
            Debug.LogError("BattleSpawnPointsが見つかりません");
            return;
        }

        foreach (BattlePlayerData data in _battlePlayerData)
        {
            if (data.connection == null)
            {
                Debug.LogError("Connectionがありません");
                continue;
            }

            if (data.characterID < 0 ||
                data.characterID >= _characterPrefabs.Count)
            {
                Debug.LogError($"不正なCharacterID: {data.characterID}");
                continue;
            }

            Transform spawnPoint =
                spawnPoints.GetSpawnPoint(data.playerNumber);

            if (spawnPoint == null)
            {
                Debug.LogError(
                    $"{data.playerNumber}P のSpawnPointがありません");
                continue;
            }

            GameObject newPlayer = Instantiate(
                _characterPrefabs[data.characterID],
                spawnPoint.position,
                spawnPoint.rotation
            );

            ConnectPlayerNumber newPlayerNumber =
                newPlayer.GetComponent<ConnectPlayerNumber>();

            if (newPlayerNumber != null)
            {
                newPlayerNumber.SetPlayerNumber(data.playerNumber);
            }

            NetworkServer.ReplacePlayerForConnection(
                data.connection,
                newPlayer,
                ReplacePlayerOptions.KeepAuthority
            );
        }

        PercentageUIManager percentageUIMgr =
    FindFirstObjectByType<PercentageUIManager>();

        percentageUIMgr.RpcSetInitialPlayerUI();

        _battlePlayerData.Clear();
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