using Mirror;
using System;
using UnityEngine;

public class CharacterSelectPlayer : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnCharacterChanged))]
    private int _characterID = -1;

    public int CharacterID => _characterID;

    // UIのキャラ選択ボタンから呼ぶ
    public void SelectCharacter(int characterID)
    {
        // 自分のPlayer以外からは送れない
        if (!isLocalPlayer)
            return;

        CmdSelectCharacter(characterID);
    }

    [Command]
    private void CmdSelectCharacter(int characterID)
    {
        _characterID = characterID;
    }

    private void OnCharacterChanged(int oldID, int newID)
    {
        Debug.Log($"{GetComponent<ConnectPlayerNumber>().PlayerNumber}P が Character {newID} を選択");
    }

    [SyncVar] private bool _isReady = false;

    public bool IsReady => _isReady;

    public void Ready()
    {
        if (!isLocalPlayer)
            return;

        if (_characterID < 0)
        {
            return;
        }

        CmdReady();
    }

    [Command]
    private void CmdReady()
    {
        _isReady = true;

        GameNetworkManager manager =
    NetworkManager.singleton as GameNetworkManager;

        manager?.CheckAllReady();
    }
}