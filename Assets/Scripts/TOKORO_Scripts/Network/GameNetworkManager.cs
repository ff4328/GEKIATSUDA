using Mirror;
using Utp;
using System.Collections.Generic;

public class GameNetworkManager : RelayNetworkManager
{
    private readonly List<ConnectPlayerNumber> players = new();

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        ConnectPlayerNumber player =
            conn.identity.GetComponent<ConnectPlayerNumber>();

        if (player != null)
        {
            players.Add(player);
        }
    }
}