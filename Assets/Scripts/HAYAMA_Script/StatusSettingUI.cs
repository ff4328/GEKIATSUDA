using UnityEngine;
using TMPro;

public class StatusSettingUI : MonoBehaviour
{
    public ConnectPlayerNumbers player;

    public TextMeshProUGUI attackText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI sizeText;

    private int maxLimit = 5;          // 初期上限
    private int maxTotal = 7;          // 全体の最大ポイント
    private int clickCount = 0;        // 上限解放用クリック数

    void Start()
    {
        RefreshUI();
    }

    // 現在の合計ポイント
    int TotalPoints()
    {
        return player.selectedAttack +
               player.selectedSpeed +
               player.selectedSize;
    }

    // ＋ボタン（攻撃）
    public void AddAttack()
    {
        if (player.selectedAttack < maxLimit && TotalPoints() < maxTotal)
        {
            player.selectedAttack++;
        }
        else if (player.selectedAttack >= maxLimit)
        {
            clickCount++;

            if (clickCount >= 10)
            {
                maxLimit = 7;   // 上限解放
                clickCount = 0;
            }
        }

        RefreshUI();
    }

    // ー（攻撃）
    public void SubAttack()
    {
        if (player.selectedAttack > 0)
            player.selectedAttack--;

        RefreshUI();
    }

    // ＋ボタン（速度）
    public void AddSpeed()
    {
        if (player.selectedSpeed < maxLimit && TotalPoints() < maxTotal)
        {
            player.selectedSpeed++;
        }
        else if (player.selectedSpeed >= maxLimit)
        {
            clickCount++;

            if (clickCount >= 10)
            {
                maxLimit = 7;
                clickCount = 0;
            }
        }

        RefreshUI();
    }

    // ー（速度）
    public void SubSpeed()
    {
        if (player.selectedSpeed > 0)
            player.selectedSpeed--;

        RefreshUI();
    }

    // ＋ボタン（サイズ）
    public void AddSize()
    {
        if (player.selectedSize < maxLimit && TotalPoints() < maxTotal)
        {
            player.selectedSize++;
        }
        else if (player.selectedSize >= maxLimit)
        {
            clickCount++;

            if (clickCount >= 10)
            {
                maxLimit = 7;
                clickCount = 0;
            }
        }

        RefreshUI();
    }

    // ー（サイズ）
    public void SubSize()
    {
        if (player.selectedSize > 0)
            player.selectedSize--;

        RefreshUI();
    }

    void RefreshUI()
    {
        attackText.text = $"Attack: {player.selectedAttack}/{maxLimit}";
        speedText.text = $"Speed: {player.selectedSpeed}/{maxLimit}";
        sizeText.text = $"Size: {player.selectedSize}/{maxLimit}";
    }
}
