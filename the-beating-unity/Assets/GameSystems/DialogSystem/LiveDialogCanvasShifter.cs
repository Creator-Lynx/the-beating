using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class LiveDialogCanvasShifter : MonoBehaviour
{
  
    [SerializeField] Vector2 shiftVector;
    [SerializeField] float smoothTime = 0.1f;
    [SerializeField] Vector3 defaultShift;
    Transform _realCamera;
    Transform _parentCamera;
    float _distanceToCanvas;
    float _scaleCorrect;
    [SerializeField] Transform motherObjectToGetScale;

    void Awake()
    {
        defaultShift = transform.localPosition;
        _realCamera = Camera.main.transform;
        _parentCamera = transform.parent;
        _distanceToCanvas = (transform.position - _parentCamera.transform.position).magnitude;
        _scaleCorrect = motherObjectToGetScale.transform.lossyScale.x;
    }
    Vector3 currentShiftVelosity;
    void Update()
    {
        float x = Mouse.current.position.ReadValue().x / Screen.width * 2 - 1;
        float y = Mouse.current.position.ReadValue().y / Screen.height * 2 - 1;

        Vector3 camerasDelta = 
        _realCamera.position + _realCamera.transform.forward * _distanceToCanvas - 
                                    (_parentCamera.position + _parentCamera.transform.forward * _distanceToCanvas);
        camerasDelta /= _scaleCorrect;

        Vector3 targetPos = defaultShift; //+ camerasDelta.ProjectOntoPlane(_parentCamera.transform.forward);
        targetPos.x = x * shiftVector.x;
        targetPos.y = y * shiftVector.y;

        targetPos += camerasDelta.ProjectOntoPlane(_parentCamera.transform.forward);

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPos, ref currentShiftVelosity, smoothTime);
    }
}
