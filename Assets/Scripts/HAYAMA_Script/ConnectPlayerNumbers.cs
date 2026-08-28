using Mirror;
using UnityEngine;

public class ConnectPlayerNumbers : NetworkBehaviour
{
    [SyncVar] public int PlayerNumber;

    // ★プレイヤーが設定シーンで選ぶステータス
    [SyncVar] public int selectedAttack;
    [SyncVar] public int selectedSpeed;
    [SyncVar] public int selectedSize;

    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log($"Player {PlayerNumber} joined.");
    }
}
