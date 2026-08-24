using System;
using UnityEngine;

public class Heal_Effect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    [SerializeField] GameObject HealUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            TestHealEffect();

        }
    }

    bool TestHealEffect()
    {

      HealUI.SetActive(true);

        return false;
    }
}
