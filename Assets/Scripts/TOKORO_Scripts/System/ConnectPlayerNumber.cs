using Mirror;
using TMPro;
using UnityEngine;

public class ConnectPlayerNumber : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnPlayerNumberChange))]
    private int _playerNumber;

    [SerializeField] private TMP_Text num;

    private bool _spawned = false;

    public int PlayerNumber => _playerNumber;

    private void OnPlayerNumberChange(int oldNumber, int newNumber)
    {
        if (num != null)
            num.text = $"{newNumber}P";
    }

    public int GetPlayerNumber()
    {
        return _playerNumber;
    }

    [Server]
    public void SetPlayerNumber(int num)
    {
        _playerNumber = num;

        SetInitialSpawnPosition();
    }

    [Server]
    private void SetInitialSpawnPosition()
    {
        // 初回だけ
        if (_spawned)
            return;

        BattleSpawnPoints spawnPoints =
            FindFirstObjectByType<BattleSpawnPoints>();

        if (spawnPoints == null)
        {
            Debug.LogWarning("BattleSpawnPointsがありません");
            return;
        }

        Transform point = spawnPoints.GetSpawnPoint(_playerNumber);

        if (point == null)
        {
            Debug.LogWarning($"{_playerNumber}PのSpawnPointがありません");
            return;
        }

        transform.SetPositionAndRotation(
            point.position,
            point.rotation
        );

        _spawned = true;
    }

    [Server]
    private void SetInitialPlayerUI()
    {
        BattleSpawnPoints spawnPoints =
            FindFirstObjectByType<BattleSpawnPoints>();

        if (spawnPoints == null)
        {
            Debug.LogWarning("BattleSpawnPointsがありません");
            return;
        }

        Transform point = spawnPoints.GetSpawnPoint(_playerNumber);

        if (point == null)
        {
            Debug.LogWarning($"{_playerNumber}PのSpawnPointがありません");
            return;
        }

        transform.SetPositionAndRotation(
            point.position,
            point.rotation
        );

        _spawned = true;
    }
}