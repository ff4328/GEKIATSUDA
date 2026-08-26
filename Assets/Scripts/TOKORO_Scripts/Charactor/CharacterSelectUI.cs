using Mirror;
using UnityEngine;

public class CharacterSelectUI : MonoBehaviour
{
    private CharacterSelectPlayer localPlayer;

    private void Start()
    {
        FindLocalPlayer();
    }

    private void FindLocalPlayer()
    {
        foreach (var player in FindObjectsByType<CharacterSelectPlayer>(
                     FindObjectsSortMode.None))
        {
            if (player.isLocalPlayer)
            {
                localPlayer = player;
                break;
            }
        }
    }

    public void SelectCharacter(int characterID)
    {
        if (localPlayer == null)
        {
            FindLocalPlayer();

            if (localPlayer == null)
            {
                Debug.LogWarning("LocalPlayerが見つかりません");
                return;
            }
        }

        localPlayer.SelectCharacter(characterID);
    }

    public void Ready()
    {
        if (localPlayer == null)
        {
            FindLocalPlayer();

            if (localPlayer == null)
            {
                Debug.LogWarning("LocalPlayerが見つかりません");
                return;
            }
        }

        localPlayer.Ready();
    }
}