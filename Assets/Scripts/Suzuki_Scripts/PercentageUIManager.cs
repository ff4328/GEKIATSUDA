
using TMPro;
using UnityEngine;

public class PercentageUIManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI textPrefab;   // ★HP表示用のプレハブ
    [SerializeField] Transform parent;             // ★Horizontal Layout Group の親

    BaseCharacter[] characters;
    TextMeshProUGUI[] texts;


    private void Start()
    {
        // ★シーン内の BaseCharacter を全部自動取得
        characters = FindObjectsByType<BaseCharacter>(FindObjectsSortMode.None);

        // ★キャラ数に応じて UI を自動生成
        texts = new TextMeshProUGUI[characters.Length];

        for (int i = 0; i < characters.Length; i++)
        {
            // Text を生成して Horizontal の子にする
            var t = Instantiate(textPrefab, parent);
            t.text = "0.0"; // 初期値
            texts[i] = t;
        }
    }
    private void Update()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            float p = characters[i].data.Percentage;

            // HP表示
            texts[i].text = p.ToString("F1");

            // 色変化
            if (p <= 50)
            {
                float t = (p - 20.0f) / 50.0f;
                texts[i].color = Color.Lerp(Color.white, Color.yellow, t);
            }
            else
            {
                float t = (p - 50.0f) / 50.0f;
                texts[i].color = Color.Lerp(Color.yellow, Color.red, t);
            }
        }

        /*
        float percentages = characters.Percentage;
        for (int i = 0; i < textPercentages.Length; i++)
        {
            textPercentages[i].text = percentages.ToString("F1");


            
             １５から黄色に代わっていって
              ５０まで黄色からオレンジに変わっていって
                １００まで赤に川廷ってる
             


            if (percentages <= 50)
            {
                float ColorChange = (percentages - 20.0f) / 50.0f;
                textPercentages[i].color =
                Color.Lerp(Color.white, Color.yellow, ColorChange);
            }
            else
            {
                float ColorChange = (percentages - 50.0f) / 50.0f;
                textPercentages[i].color =
                Color.Lerp(Color.yellow, Color.red, ColorChange);
            }


        }*/
    }
}