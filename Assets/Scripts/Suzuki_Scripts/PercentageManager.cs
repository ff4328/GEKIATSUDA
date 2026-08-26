using TMPro;
using UnityEngine;

public class TextColorChange : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    CharaDataBase chara;

        // キャッシュ
    float ShowPercentage;
   
    private void Update()
    {
       
        ShowPercentage = chara.Percentage;
        //パーセンテージの表示
        text.text=ShowPercentage.ToString("F1");
        //０～１００までの表示の色を白からジョジョに赤にしていく
        float t = Mathf.Clamp01(ShowPercentage / 100.0f);
        text.color = Color.Lerp(Color.white, Color.red, t);


    }

}