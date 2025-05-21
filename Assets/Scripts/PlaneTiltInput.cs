using UnityEngine;

public class PlaneTiltInput : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxAngle;

    private Vector3 rotation;

    void Update()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        if (inputHorizontal != 0 || inputVertical != 0)
        {
            float targetX = Mathf.Clamp(rotation.x - inputHorizontal * speed * Time.deltaTime, -maxAngle, maxAngle);
            float targetZ = Mathf.Clamp(rotation.z - inputVertical * speed * Time.deltaTime, -maxAngle, maxAngle);
            rotation = new Vector3(targetX, 0.0f, targetZ);
        }
        else
        {
            rotation = Vector3.MoveTowards(rotation, Vector3.zero, speed * 5.0f * Time.deltaTime);
        }

        transform.localRotation = Quaternion.Euler(rotation);
    }
}
