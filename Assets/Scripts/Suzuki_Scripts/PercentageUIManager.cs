using System;
using System.Collections;
using TMPro;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class PercentageUIManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI[] textPercentages;
    BaseCharacter[] characters;


    private void Start()
    {
        characters = FindObjectsByType<BaseCharacter>(FindObjectsSortMode.None);

        if (textPercentages.Length < characters.Length)
        {
            Debug.LogError("UIの数がキャラ数より少ないよ！");
        }
    }
    private void Update()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            float p = characters[i].data.Percentage;

            // UI更新
            textPercentages[i].text = p.ToString("F1");

            // 色変化
            if (p <= 50)
            {
                float t = (p - 20.0f) / 50.0f;
                textPercentages[i].color = Color.Lerp(Color.white, Color.yellow, t);
            }
            else
            {
                float t = (p - 50.0f) / 50.0f;
                textPercentages[i].color = Color.Lerp(Color.yellow, Color.red, t);
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