using UnityEngine;
using UnityEngine.UI;

public class holdButton : MonoBehaviour
{
    private bool isPressing;
    private float pressTime;

    private int Switch = -1;

    [SerializeField] private Image[] _coolTimeImage;

    [SerializeField] private float longPressTime = 2.0f;

    [SerializeField] DisconnectButton disconnect = new DisconnectButton();

    public void PointerDown(int value)
    {
        isPressing = true;
        pressTime = 0f;
        Switch = value;
    }

    public void PointerUp()
    {
        isPressing = false;
        _coolTimeImage[Switch].fillAmount = 0;
        pressTime = 0f;
    }

    private void Update()
    {
        if (!isPressing)
            return;

        pressTime += Time.deltaTime;

        _coolTimeImage[Switch].fillAmount = pressTime / 1;

        if (pressTime >= longPressTime)
        {
            Debug.Log("長押し成立");

            switch (Switch)
            {
                case 0:
                    disconnect.Disconnect();
                    break;
                case 1:
                    GetComponent<LobbyUI>().StartCharacterSelect();
                    break;
            }

            isPressing = false;
        }
    }
}