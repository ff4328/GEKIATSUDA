using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private BaseCharacter[] characterDatas;
    private CharacterMove[] characterPositions;
    private Vector3[] charaVectors;

    private float minX = -120f;
    private float maxX = 120f;
    private float minY = -16f;
    private float maxY = 45f;

    private float nearZ = -60f;
    private float farZ = -140f;

   
    [SerializeField] private float maxCharacterDistance = 100f;

  
    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {

        characterDatas = FindObjectsByType<BaseCharacter>(FindObjectsSortMode.None);

        characterPositions = new CharacterMove[characterDatas.Length];

        charaVectors = new Vector3[characterDatas.Length];

        for (int i = 0; i < characterDatas.Length; i++)
        {
            characterPositions[i] = characterDatas[i].GetComponent<CharacterMove>();
        }
    }

    private void Update()
    {
        if (characterPositions.Length == 0)
            return;

        // 全キャラの座標取得
        for (int i = 0; i < characterPositions.Length; i++)
        {
            if (characterPositions[i] != null)
            {
                charaVectors[i] = characterPositions[i].GetPos();
            }
        }

     

        // 一番離れている2人を探す
        float maxDistance = 0f;

        Vector3 posA = charaVectors[0];
        Vector3 posB = charaVectors[0];

        for (int i = 0; i < charaVectors.Length; i++)
        {
            for (int j = i + 1; j < charaVectors.Length; j++)
            {
                float distance =
                    Vector3.Distance(charaVectors[i], charaVectors[j]);

                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    posA = charaVectors[i];
                    posB = charaVectors[j];
                }
            }
        }

        // 中心座標
        Vector3 center = (posA + posB) * 0.5f;

        // 距離によってZを決定
        float t = Mathf.Clamp01(maxDistance / maxCharacterDistance);

        float z = Mathf.Lerp(
            nearZ,
            farZ,
            t
        );

        // カメラ座標
        Vector3 targetCameraPos = new Vector3(
            center.x,
            center.y,
            z
        );

        // 移動範囲制限
        targetCameraPos.x =
            Mathf.Clamp(targetCameraPos.x, minX, maxX);

        targetCameraPos.y =
            Mathf.Clamp(targetCameraPos.y, minY, maxY);

        targetCameraPos.z =
            Mathf.Clamp(targetCameraPos.z, farZ, nearZ);

        // なめらか移動
        transform.position = Vector3.Lerp(
            transform.position,
            targetCameraPos,
            moveSpeed * Time.deltaTime
        );
    }
}