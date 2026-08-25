using TMPro;
using UnityEngine;

public class PercentageUIManager : MonoBehaviour
{
    [SerializeField] private CharaDataBase[] characters;
    [SerializeField] private TextMeshProUGUI[] texts;


    private void Awake()
    {
        characters = FindObjectsByType<CharaDataBase>(
            FindObjectsSortMode.None
        );
    }
    private void Update()
    {
        if (characters == null)
        {
            Debug.LogError("characters配列がnullです");
            return;
        }

        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] == null)
            {
                Debug.LogError($"characters[{i}] がnullです");
                continue;
            }

            if (texts == null || i >= texts.Length)
            {
                Debug.LogError("texts配列のサイズが不足しています");
                continue;
            }

            if (texts[i] == null)
            {
                Debug.LogError($"texts[{i}] がnullです");
                continue;
            }

            float percentage = characters[i].Percentage;
            texts[i].text = percentage.ToString("F1");

            float t = Mathf.Clamp01(percentage / 100f);
            texts[i].color = Color.Lerp(Color.white, Color.red, t);
        }
    }
}