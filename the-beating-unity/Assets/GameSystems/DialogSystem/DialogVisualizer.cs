using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class DialogVisualizer : MonoBehaviour
{
    [SerializeField] CinemachineCamera cinemachineCamera;
    [SerializeField] LiveObjectShifterDialog cameraShifter;

    public bool dialogState = false;
    public void EnterDialog()
    {
        cinemachineCamera.enabled = true;
        cameraShifter.enabled = true;
        StartCoroutine(DialogStateDelay());
    }

    public void ExitDialog()
    {
        cinemachineCamera.enabled = false;
        cameraShifter.enabled = false;
        dialogState = false;
    }


    //temporary handle
    void Update()
    {
        if (dialogState) 
            if (UnityEngine.InputSystem.Keyboard.current.anyKey.wasPressedThisFrame) 
                ExitDialog();
    }

    IEnumerator DialogStateDelay()
    {
        yield return new WaitForSeconds(1f);
        dialogState = true;
    }
}
