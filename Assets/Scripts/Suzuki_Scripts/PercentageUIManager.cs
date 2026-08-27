
using Mirror;
using TMPro;
using UnityEngine;

public class PercentageUIManager : NetworkBehaviour
{

    [SerializeField] TextMeshProUGUI textPrefab;   // ★HP表示用のプレハブ

    [SerializeField] Transform parent;             // ★Horizontal Layout Group の親

    BaseCharacter[] characters;
    TextMeshProUGUI[] texts;


    // private void Start()
    // {
    //     // ★シーン内の BaseCharacter を全部自動取得
    //     characters = FindObjectsByType<BaseCharacter>(FindObjectsSortMode.None);

    //     // ★キャラ数に応じて UI を自動生成
    //     texts = new TextMeshProUGUI[characters.Length];

    //     for (int i = 0; i < characters.Length; i++)
    //     {
    //         // Text を生成して Horizontal の子にする
    //         var t = Instantiate(textPrefab, parent);
    //         t.text = "0.0"; // 初期値
    //         texts[i] = t;
    //     }


    // }

public void RefreshUI()
{
    StartUI();
}

    [ClientRpc]
public void RpcSetInitialPlayerUI()
{
    Debug.Log("⑤ ClientRpc 到着");
    StartUI();
}

    private void StartUI()
    {
        if (texts != null)
        {
            foreach (var text in texts)
            {
                if (text != null)
                {
                    Destroy(text.gameObject);
                }
            }
        }

        characters = FindObjectsByType<BaseCharacter>(
            FindObjectsSortMode.None
        );

        Debug.Log($"UI更新 Character数 = {characters.Length}");

        System.Array.Sort(characters, (a, b) =>
        {
            var aNumber = a.GetComponent<ConnectPlayerNumber>();
            var bNumber = b.GetComponent<ConnectPlayerNumber>();

            return aNumber.PlayerNumber.CompareTo(
                bNumber.PlayerNumber
            );
        });

        texts = new TextMeshProUGUI[characters.Length];

        for (int i = 0; i < characters.Length; i++)
        {
            var t = Instantiate(textPrefab, parent);
            t.text = "0.0";
            texts[i] = t;
        }
    }

    private void Update()
    {
        if (characters == null || texts == null)
            return;

        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] == null)
                continue;

            if (characters[i].data == null)
            {
                Debug.LogWarning(
                    $"{characters[i].name} の data がまだnullです"
                );
                continue;
            }

            if (i >= texts.Length || texts[i] == null)
                continue;

            float p = characters[i].Percentage;

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