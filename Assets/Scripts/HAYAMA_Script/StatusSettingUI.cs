using UnityEngine;
using UnityEngine.UI;

public class StatusSettingUI : MonoBehaviour
{
    public ConnectPlayerNumbers player;

    public Text attackText;
    public Text speedText;
    public Text sizeText;

    void Start()
    {
        RefreshUI();
    }

    // ＋ボタン
    public void AddAttack() { player.selectedAttack++; RefreshUI(); }
    public void AddSpeed() { player.selectedSpeed++; RefreshUI(); }
    public void AddSize() { player.selectedSize++; RefreshUI(); }

    // ー（減らす）ボタン
    public void SubAttack() { player.selectedAttack--; RefreshUI(); }
    public void SubSpeed() { player.selectedSpeed--; RefreshUI(); }
    public void SubSize() { player.selectedSize--; RefreshUI(); }

    void RefreshUI()
    {
        attackText.text = $"攻撃: {player.selectedAttack}";
        speedText.text = $"速度: {player.selectedSpeed}";
        sizeText.text = $"サイズ: {player.selectedSize}";
    }
}
