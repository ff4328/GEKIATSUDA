using UnityEngine;

public class SwordMan : MonoBehaviour
{
    private CharaDataBase data;

    void Start()
    {
        data = new CharaDataBase();
        SetUp();
    }

    void SetUp()
    {
        data.SetPercentage(0);
        data.SetAttack(7);
        data.SetSpeed(1);
        data.SetSize(1);
        data.SetJumpPower(2);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            Debug.Log(data.GetAttack());
    }

}