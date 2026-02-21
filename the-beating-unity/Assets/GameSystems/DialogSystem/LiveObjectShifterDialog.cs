using UnityEngine;
using UnityEngine.InputSystem;

public class LiveObjectShifterDialog : MonoBehaviour
{
  
    [SerializeField] Vector2 shiftVector;
    [SerializeField] float smoothTime = 0.1f;
    [SerializeField] Vector3 defaultShift;


    void Awake()
    {
        defaultShift = transform.position;
    }
    Vector3 currentShiftVelosity;
    void Update()
    {
        float x = Mouse.current.position.ReadValue().x / Screen.width * 2 - 1;
        float y = Mouse.current.position.ReadValue().y / Screen.height * 2 - 1;

        Vector3 targetPos = defaultShift;
        targetPos += x * shiftVector.x * transform.right;
        targetPos += y * shiftVector.y * transform.up;
        //targetPos.x = defaultShift.x + ;
        //targetPos.y = defaultShift.y + ;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentShiftVelosity, smoothTime);
    }
}
