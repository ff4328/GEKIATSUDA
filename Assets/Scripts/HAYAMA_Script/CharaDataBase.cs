using UnityEngine;
using System;

public class CharaDataBase 
{

    private Shotdown_Effect effect;

    private void Start()
    {
        EffectManager manager =
            MonoBehaviour.FindFirstObjectByType<EffectManager>();

        effect = new Shotdown_Effect(manager);
    }

    public float Percentage { get; private set; }
    public int Attack { get; private set; }
    public float Speed { get; set; }
    public int Size { get; private set; }
    public int JumpPower { get; private set; }
    public float LaunchRate { get; private set; }
    public bool IsTakeDamage { get; private set; }

   
    public void SetPercentage(float percentage)
    {
        Percentage = percentage;
        UpdateLaunchRate();
    }
    public void SetAttack(int attack)
    {
        Attack = attack + DataConst.ATTACK;
    }
    public void SetSpeed(float speed)
    {
        Speed = speed + DataConst.SPEED;
    }
    public void SetSize(int size)
    {
        Size = size + DataConst.SIZE;
        UpdateJumpPower();
        UpdateLaunchRate();
    }
    public void SetJumpPower(int jumpPower)
    {
        JumpPower = (jumpPower + DataConst.JUMPPOWER) / Size;
    }
    private void UpdateJumpPower()
    {
        JumpPower = (DataConst.JUMPPOWER) / Size;
    }
    private void UpdateLaunchRate()
    {
        LaunchRate = (Percentage * DataConst.LAUNCHRATE) / Size;
    }
    public void TakeDamage(int attackPower)
    {
        Percentage += attackPower;
        UpdateLaunchRate();
        IsTakeDamage = true;
    }

    public void Dead()
    {
        Percentage = 0;
        LaunchRate = 0;
        UpdateLaunchRate();
        Debug.Log("Character is dead.");
    }
    public void Heal(int heal)
    {
        if (Percentage <= 0)
        {
            Percentage = 0;
        }
        Percentage -= heal;
        UpdateLaunchRate();
    }
    public void SmashDead()
    {
        if (Percentage >= 200)
        {
            LaunchRate += LaunchRate * LaunchRate;
            UpdateLaunchRate();
            effect.Shotdown(effect.PlayerPos());
        }
    }
}
