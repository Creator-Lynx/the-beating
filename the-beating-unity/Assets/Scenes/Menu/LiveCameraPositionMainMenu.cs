using UnityEngine;
using UnityEngine.InputSystem;

public class LiveCameraPositionMainMenu : MonoBehaviour
{
  
    [SerializeField] Vector2 cameraShiftVector;
    [SerializeField] float smoothTime = 0.1f;


    Vector3 currentShiftVelosity;
    void Update()
    {
        float x = Mouse.current.position.ReadValue().x / Screen.width * 2 - 1;
        Vector3 targetPos = transform.position;
        targetPos.x = x * cameraShiftVector.x;

        float y = Mouse.current.position.ReadValue().y / Screen.height * 2 - 1;
        targetPos.y = 1 + y * cameraShiftVector.y;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentShiftVelosity, smoothTime);
    }
}
