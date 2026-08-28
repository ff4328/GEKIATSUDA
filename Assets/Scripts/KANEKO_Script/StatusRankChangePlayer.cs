using Mirror;
using UnityEngine;

public class StatusRankChangePlayer : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnAttackRankChanged))]
    private int _attackRank = 1;

    [SyncVar(hook = nameof(OnSpeedRankChanged))]
    private int _speedRank = 1;

    [SyncVar(hook = nameof(OnSizeRankChanged))]
    private int _sizeRank = 1;

    public int AttackRank => _attackRank;
    public int SpeedRank => _speedRank;
    public int SizeRank => _sizeRank;

    // UIのステータス変更ボタンから呼ぶ
    public void ChangeAttackRank(int attackRank)
    {
        // 自分のPlayer以外からは送れない
        if (!isLocalPlayer)
            return;

        CmdChangeAttackRank(attackRank);
    }

    // UIのステータス変更ボタンから呼ぶ
    public void ChangeSpeedRank(int speedRank)
    {
        // 自分のPlayer以外からは送れない
        if (!isLocalPlayer)
            return;

        CmdChangeSpeedRank(speedRank);
    }

    // UIのステータス変更ボタンから呼ぶ
    public void ChangeSizeRank(int sizeRank)
    {
        // 自分のPlayer以外からは送れない
        if (!isLocalPlayer)
            return;

        CmdChangeSizeRank(sizeRank);
    }

    [Command]
    private void CmdChangeAttackRank(int attackRank)
    {
        _attackRank += attackRank;
        _attackRank = Mathf.Clamp(_attackRank, 1, 5);
    }

    [Command]
    private void CmdChangeSpeedRank(int speedRank)
    {
        _speedRank += speedRank;
        _speedRank = Mathf.Clamp(_speedRank, 1, 5);
    }

    [Command]
    private void CmdChangeSizeRank(int sizeRank)
    {
        _sizeRank += sizeRank;
        _sizeRank = Mathf.Clamp(_sizeRank, 1, 5);
    }

    private void OnAttackRankChanged(int oldRank, int newRank)
    {
        Debug.Log($"{GetComponent<ConnectPlayerNumber>().PlayerNumber}P が AttackRank を {newRank} に変更");
    }
    private void OnSpeedRankChanged(int oldRank, int newRank)
    {
        Debug.Log($"{GetComponent<ConnectPlayerNumber>().PlayerNumber}P が SpeedRank を {newRank} に変更");
    }

    private void OnSizeRankChanged(int oldRank, int newRank)
    {
        Debug.Log($"{GetComponent<ConnectPlayerNumber>().PlayerNumber}P が SizeRank を {newRank} に変更");
    }
}
