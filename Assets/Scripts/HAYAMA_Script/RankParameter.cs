using UnityEngine;

[CreateAssetMenu(fileName = "RankParameter", menuName = "Game/RankParameter")]
public class RankParameter : ScriptableObject
{
    public int attackRank = 3;
    public float speedRank = 3;
    public int sizeRank = 3;

    public int GetAttack(int baseAttack)
    {
        switch (attackRank)
        {
            case 1: return baseAttack;
            case 2: return baseAttack + 2;
            case 3: return baseAttack + 5;
            case 4: return baseAttack + 9;
            case 5: return baseAttack + 14;
        }
        return baseAttack;
    }

    public float GetSpeed(float baseSpeed)
    {
        switch (speedRank)
        {
            case 1: return baseSpeed;
            case 2: return baseSpeed + 1;
            case 3: return baseSpeed + 2;
            case 4: return baseSpeed + 3;
            case 5: return baseSpeed + 5;
        }
        return baseSpeed;
    }

    public int GetSize(int baseSize)
    {
        switch (sizeRank)
        {
            case 1: return baseSize;
            case 2: return baseSize + 1;
            case 3: return baseSize + 2;
            case 4: return baseSize + 3;
            case 5: return baseSize + 5;
        }
        return baseSize;
    }
}
