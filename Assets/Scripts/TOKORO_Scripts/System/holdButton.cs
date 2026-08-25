using UnityEngine;
using UnityEngine.UI;

public class holdButton : MonoBehaviour
{
    private bool isPressing;
    private float pressTime;
    [SerializeField] private Image _coolTimeImage;

    [SerializeField] private float longPressTime = 2.0f;

    [SerializeField] DisconnectButton disconnect = new DisconnectButton();

    public void PointerDown()
    {
        isPressing = true;
        pressTime = 0f;
    }

    public void PointerUp()
    {
        isPressing = false;
        _coolTimeImage.fillAmount = 0;
        pressTime = 0f;
    }

    private void Update()
    {
        if (!isPressing)
            return;

        pressTime += Time.deltaTime;

        _coolTimeImage.fillAmount = pressTime/1;

        if (pressTime >= longPressTime)
        {
            Debug.Log("長押し成立");

            disconnect.Disconnect();

            isPressing = false;
        }
    }
}
