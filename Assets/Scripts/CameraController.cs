using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform objetivo;
    public float cameraSpeed = 0.05f;
    public Vector3 desplazamiento;

    private void LateUpdate()
    {
        transform.position = objetivo.position + desplazamiento;
    }



}
