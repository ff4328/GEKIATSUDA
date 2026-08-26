using Mirror;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    public void StartCharacterSelect()
    {
        if (!NetworkServer.active)
        {
            Debug.LogWarning("Hostだけがシーンを変更できます");
            return;
        }

        NetworkManager.singleton.ServerChangeScene("CharacterSelectScene");
        //NetworkManager.singleton.ServerChangeScene("Cave");
    }
}