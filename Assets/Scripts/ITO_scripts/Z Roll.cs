using UnityEngine;

public class ZRoll : MonoBehaviour
{
    [SerializeField] private float rotateAngle = 360f;
    [SerializeField] private float speed = 2f;

    private Quaternion startRotation;

    private void Start()
    {
        startRotation = transform.rotation;
    }

    private void Update()
    {
        float z = Mathf.PingPong(Time.time * speed, rotateAngle);

        transform.rotation = startRotation * Quaternion.Euler(0f, 0f, z);
    }
}
