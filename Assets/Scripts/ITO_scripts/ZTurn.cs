using UnityEngine;

public class ZTurn: MonoBehaviour
{
    [SerializeField] private float rotateAngle = 45f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotateSpeed = 90f;
    private Quaternion startRotation;

    private void Start()
    {
        startRotation = transform.rotation;
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }
}
