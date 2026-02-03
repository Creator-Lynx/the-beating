using UnityEngine;
using UnityEngine.InputSystem;

public class LiveObjectShifterMainMenu : MonoBehaviour
{
  
    [SerializeField] Vector2 shiftVector;
    [SerializeField] float smoothTime = 0.1f;
    [SerializeField] Vector2 defaultShift;


    Vector3 currentShiftVelosity;
    void Update()
    {
        float x = Mouse.current.position.ReadValue().x / Screen.width * 2 - 1;
        Vector3 targetPos = transform.position;
        targetPos.x =defaultShift.x + x * shiftVector.x;

        float y = Mouse.current.position.ReadValue().y / Screen.height * 2 - 1;
        targetPos.y = defaultShift.y + y * shiftVector.y;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentShiftVelosity, smoothTime);
    }
}
