using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通り抜け可能な床
/// </summary>
public class CanPassFloor : StageGimmickBase
{
    //コリジョンを無効にする時間
    private const float _COLLISION_IGNORE_TIME = 1.1f;


    /// <summary>
    /// 無効にしているコリジョンのデータ
    /// </summary>
    private class IgnoreCollisionData
    {
        public IgnoreCollisionData(Collider collider)
        {
            this.collider = collider;
            ignoreTimeSec = 0;
        }

        //無効にするコライダー
        public Collider collider;
        //無効にしている時間
        public float ignoreTimeSec;

    }

    //無効なコリジョンのデータリスト
    private List<IgnoreCollisionData> _ignoreCollisionList;

    protected override void Start()
    {
        base.Start();
        _ignoreCollisionList = new List<IgnoreCollisionData>();
    }

    private void Update()
    {
        //空かチェック
        if (_ignoreCollisionList.Count <= 0) return;

        float deltaTime = Time.deltaTime;

        for(int i = 0; i < _ignoreCollisionList.Count; i++)
        {
            IgnoreCollisionData checkData = _ignoreCollisionList[i];

            //経過時間を加算
            checkData.ignoreTimeSec += deltaTime;
            //コリジョンを有効にするタイミングでなければ処理しない
            if (checkData.ignoreTimeSec < _COLLISION_IGNORE_TIME) continue;
            //衝突判定を有効
            Physics.IgnoreCollision(checkData.collider, collider, false);
            //配列から削除する
            _ignoreCollisionList.Remove(checkData);

        }

    }

    private void AddIgnoreCollisionList(BaseCharacter hitCharacter)
    {
        //無効にするコリジョンを取得
        Collider[] ignoreCollisionList = hitCharacter.characterMove.GetColliders();
        for (int i = 0; i < ignoreCollisionList.Length; i++)
        {
            Collider ignoreCollider = ignoreCollisionList[i];
            if (ignoreCollider == null) continue;

            //コリジョンを無効にする
            Physics.IgnoreCollision(ignoreCollider, collider, true);

            //無効にするコリジョンリストのデータ作成
            IgnoreCollisionData newData = new IgnoreCollisionData(ignoreCollider);
            //無効にするコリジョンリストに追加
            _ignoreCollisionList.Add(newData);
        }
    }

    public override void HitToCharacter(BaseCharacter hitCharacter)
    {
        if (hitCharacter == null || collider == null) return;
        //もし移動方向が下ならコリジョンを無効にする
        if (hitCharacter.characterMove.GetMoveValue().y < 0)
        {
            //無効なコリジョンのリストに登録
            AddIgnoreCollisionList(hitCharacter);
        }

    }

}
