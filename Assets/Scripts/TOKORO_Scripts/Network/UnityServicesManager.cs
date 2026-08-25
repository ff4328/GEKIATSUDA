using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Cysharp.Threading.Tasks;
using System;

public class UnityServicesManager : MonoBehaviour
{
    private async UniTask Awake()
    {
        await UnityServices.InitializeAsync();
        
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in to Unity Services");
        };

        await SignInAnonymouslyAsync();
    }

    private async UniTask SignInAnonymouslyAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Signed in anonymously (completed)");
        }
        catch (Exception e)
        {
            Debug.Log($"Failed to sign in anonymously: {e}");
        }
    }
}