using System;
using TMPro;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class PercentageUIManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI[] textPercentages;
    [SerializeField] public CharaDataBase characters;
    float percentages;


    private void Awake()
    {
     


        characters = new CharaDataBase();





    }
    private void Update()
    {

        percentages = characters.Percentage;

        //percentages = characters.Percentage;
        //debug用
        if (Input.GetKeyDown(KeyCode.S))
        {
            characters.TakeDamage(1);
            Debug.Log(characters.Percentage);
        }
        for (int i = 0; i < textPercentages.Length; i++)
        {
            textPercentages[i].text = percentages.ToString("F1");


            /*
             １５から黄色に代わっていって
              ５０まで黄色からオレンジに変わっていって
                １００まで赤に川廷ってる
             */


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


        }

    }
}