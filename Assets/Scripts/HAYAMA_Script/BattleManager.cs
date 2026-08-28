using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;

public class BattleManager : NetworkBehaviour
{
    public static BattleManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // 全キャラ
    private List<BaseCharacter> allCharacters = new List<BaseCharacter>();

    // 死亡順
    private List<BaseCharacter> deathOrder = new List<BaseCharacter>();

    private bool isGameFinished = false;

    // ★ BaseCharacter が spawn されたら登録する
    public void RegisterCharacter(BaseCharacter character)
    {
        if (!allCharacters.Contains(character))
            allCharacters.Add(character);
    }

    // ★ 死亡した瞬間に呼ばれる
    public void OnCharacterDead(BaseCharacter character)
    {
        if (!deathOrder.Contains(character))
        {
            deathOrder.Add(character);
        }
    }

    void Update()
    {
        if (!isServer) return;
        if (isGameFinished) return;

        // 生存人数チェック
        int aliveCount = allCharacters.Count(c => c.data.PlayerHP > 0);

        // ★最後の1人になったら勝敗確定
        if (aliveCount == 1)
        {
            isGameFinished = true;

            // 最後の生存者を順位の最後に追加
            var winner = allCharacters.First(c => c.data.PlayerHP > 0);
            deathOrder.Add(winner);

            // 順位を名前で送る（キャラIDでもOK）
            string[] rankingNames = deathOrder
                .Select(c => c.name)
                .ToArray();

            RpcGoToResult(rankingNames);
        }
    }

    [ClientRpc]
    void RpcGoToResult(string[] rankingNames)
    {
        // リザルト画面に渡す
        ResultSceneData.ranking = rankingNames;

        UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
    }
}
