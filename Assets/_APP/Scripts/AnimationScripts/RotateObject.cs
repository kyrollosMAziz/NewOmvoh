using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Rotation speed in degrees per second for each axis
    public Vector3 rotationSpeed = new Vector3(0f, 100f, 0f);

    void Update()
    {
        // Rotate the object around its local axes based on the defined rotation speed
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
