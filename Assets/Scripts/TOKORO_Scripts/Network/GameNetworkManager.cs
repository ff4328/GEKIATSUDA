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

    private readonly List<NetworkConnectionToClient> players = new();

    [SerializeField] private List<GameObject> _characterPrefabs = new();




    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        if (!players.Contains(conn))
        {
            players.Add(conn);
        }

        ApplyPlayerNumbers();
    }

    [Server]
    private void ApplyPlayerNumbers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            NetworkConnectionToClient conn = players[i];

            if (conn == null || conn.identity == null)
                continue;

            ConnectPlayerNumber playerNumber =
                conn.identity.GetComponent<ConnectPlayerNumber>();

            if (playerNumber != null)
            {
                playerNumber.SetPlayerNumber(i + 1);
            }
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
        players.RemoveAll(conn => conn == null);

        if (players.Count == 0)
            return;

        // 全員Readyか確認
        foreach (NetworkConnectionToClient conn in players)
        {
            if (conn.identity == null)
                return;

            CharacterSelectPlayer selectPlayer =
                conn.identity.GetComponent<CharacterSelectPlayer>();

            ConnectPlayerNumber playerNumber =
                conn.identity.GetComponent<ConnectPlayerNumber>();

            if (selectPlayer == null)
            {
                Debug.LogWarning("CharacterSelectPlayerがありません");
                return;
            }

            if (!selectPlayer.IsReady)
            {
                Debug.Log(
                    $"{playerNumber.PlayerNumber}P はまだReadyしていません"
                );
                return;
            }
        }

        Debug.Log("全員Ready！");

        // 戦闘シーンへ持っていく情報を保存
        _battlePlayerData.Clear();

        foreach (NetworkConnectionToClient conn in players)
        {
            if (conn.identity == null)
                continue;

            ConnectPlayerNumber playerNumber =
                conn.identity.GetComponent<ConnectPlayerNumber>();

            CharacterSelectPlayer select =
                conn.identity.GetComponent<CharacterSelectPlayer>();

            if (playerNumber == null || select == null)
                continue;

            _battlePlayerData.Add(new BattlePlayerData
            {
                connection = conn,
                playerNumber = playerNumber.PlayerNumber,
                characterID = select.CharacterID
            });
        }

        ServerChangeScene("Normal");
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

        if (sceneName == "Normal")
        {
            SpawnBattlePlayers();
        }
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        players.Remove(conn);

        base.OnServerDisconnect(conn);

        ApplyPlayerNumbers();
    }
}