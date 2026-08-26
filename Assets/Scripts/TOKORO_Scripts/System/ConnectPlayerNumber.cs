using Mirror;
using TMPro;
using UnityEngine;

public class ConnectPlayerNumber : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnPlayerNumberChange))] private int _playerNumber;

    [SerializeField] TMP_Text num;

    public int PlayerNumber => _playerNumber;

    private void OnPlayerNumberChange(int oldNumber, int newNumber)
    {
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
    }
}