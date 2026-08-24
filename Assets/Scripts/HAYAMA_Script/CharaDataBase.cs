public class CharaDataBase
{
    protected float Percentage;
    protected int Attack;
    protected int Speed;
    protected int Size;
    protected int JumpPower;
    protected float LaunchRate;

    public void SetPercentage(float percentage)
    {
        Percentage = percentage;
        UpdateLaunchRate();   // 自動更新
    }

    public void SetAttack(int attack)
    {
        Attack = attack + DataConst.ATTACK;
    }

    public void SetSpeed(int speed)
    {
        Speed = speed + DataConst.SPEED;
    }

    public void SetSize(int size)
    {
        Size = size + DataConst.SIZE;
        UpdateJumpPower();    // 自動更新
        UpdateLaunchRate();   // 自動更新
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

    public int GetAttack()
    {
        return Attack;
    }
}