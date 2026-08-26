using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ControllerLongPress : MonoBehaviour
{
    private float pressTime;
    private bool longPressed;

    [SerializeField] private float longPressTime = 2.0f;
    [SerializeField] private DisconnectButton disconnect;
    [SerializeField] private Image _coolTimeImage;

    private void Update()
    {
        if (Gamepad.current == null)
            return;

        // LRボタンを押している間
        if(Gamepad.current.leftTrigger.isPressed&& Gamepad.current.rightTrigger.isPressed)
        {
            pressTime += Time.deltaTime;

            _coolTimeImage.fillAmount = pressTime / 1;

            if (pressTime >= longPressTime && !longPressed)
            {
                longPressed = true;

                _coolTimeImage.fillAmount = 0;

                GetComponent<LobbyUI>().StartCharacterSelect();

                Debug.Log("長押し！");
            }
        }
        else if (Gamepad.current.buttonNorth.isPressed)
        {
            pressTime += Time.deltaTime;

            _coolTimeImage.fillAmount = pressTime / 1;

            if (pressTime >= longPressTime && !longPressed)
            {
                longPressed = true;

                _coolTimeImage.fillAmount = 0;

                disconnect.Disconnect();

                Debug.Log("長押し！");
            }
        }
        else
        {
            pressTime = 0f;
            longPressed = false;
            _coolTimeImage.fillAmount = 0;
        }
    }
}