public class CharaDataBase
{
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
        // 攻撃力分だけパーセンテージを増やす
        Percentage += attackPower;

        // 吹っ飛び率を再計算
        UpdateLaunchRate();

        IsTakeDamage = true;
    }

}