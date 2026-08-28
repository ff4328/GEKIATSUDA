using TMPro;
using UnityEngine;

public class StatusRankChangeUI : MonoBehaviour
{
    private StatusRankChangePlayer localPlayer;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI sizeText;

    private int maxLimit = 5;          // 初期上限


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindLocalPlayer();
        RefreshUI();
    }
    private void FindLocalPlayer()
    {
        foreach (var player in FindObjectsByType<StatusRankChangePlayer>(
                     FindObjectsSortMode.None))
        {
            if (player.isLocalPlayer)
            {
                localPlayer = player;
                break;
            }
        }
    }

    public void ChangeAttackRank(int attackRank)
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

        localPlayer.ChangeAttackRank(attackRank);
        Debug.Log("LocalPlayerが見つかりました");
        RefreshUI();
    }
    public void ChangeSpeedRank(int speedRank)
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

        localPlayer.ChangeSpeedRank(speedRank);
        RefreshUI();
    }
    public void ChangeSizeRank(int speedRank)
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

        localPlayer.ChangeSizeRank(speedRank);
        RefreshUI();
    }

    void RefreshUI()
    {
        attackText.text = $"Attack: {localPlayer.AttackRank}/{maxLimit}";
        speedText.text = $"Speed: {localPlayer.SpeedRank}/{maxLimit}";
        sizeText.text = $"Size: {localPlayer.SizeRank}/{maxLimit}";
    }

}
