using UnityEngine;
using Unity.Services.Multiplayer;
using Cysharp.Threading.Tasks;

public class RelayManager : MonoBehaviour
{
   [SerializeField] private int _maxPlayers=7;

    public void HostSetup()
    {
        StartRelayHost().Forget();
    }

    private async UniTask StartRelayHost()
    {
        SessionOptions options = new SessionOptions();

        options.MaxPlayers = _maxPlayers;

        options.WithRelayNetwork();

        options.WithNetworkHandler(new MirrorNetworkHandler());

        await CreateSessionAsync(options);
    }

    private async UniTask CreateSessionAsync(SessionOptions options)
    {
        var session = await MultiplayerService.Instance.CreateSessionAsync(options);

        Debug.Log(session.Code);
    }
}