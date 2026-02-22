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
        

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void ExitDialog()
    {
        cinemachineCamera.enabled = false;
        cameraShifter.enabled = false;
        dialogState = false;
        StartCoroutine(PlayerPlayDelay());

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        yield return new WaitForSeconds(0.2f);
        dialogState = true;
        PlayerPauseSystem.Pause();
    }

    IEnumerator PlayerPlayDelay()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerPauseSystem.Play();
    }
}
