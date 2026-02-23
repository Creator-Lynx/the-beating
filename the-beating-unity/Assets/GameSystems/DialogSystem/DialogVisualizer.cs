using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class DialogVisualizer : MonoBehaviour
{
    [SerializeField] CinemachineCamera cinemachineCamera;
    [SerializeField] LiveObjectShifterDialog cameraShifter;

    [SerializeField] AnimationCurve _fogDensityChanging;
    [SerializeField] AnimationCurve _fogDensityChangingOut;
    [SerializeField] AnimationCurve _skyExposureChanging;
    [SerializeField] Material skybox;
    [SerializeField] float timeToFogChange = 1f;
  

    public bool dialogState = false;
    public void EnterDialog()
    {
        cinemachineCamera.enabled = true;
        cameraShifter.enabled = true;
        StartCoroutine(DialogStateDelay());
        

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        StartCoroutine(IncreaseFogDensity());
    }

    public void ExitDialog()
    {
        cinemachineCamera.enabled = false;
        cameraShifter.enabled = false;
        dialogState = false;
        StartCoroutine(PlayerPlayDelay());

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(DecreaseFogDensity());
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

    IEnumerator IncreaseFogDensity()
    {
        float timer = 0f;
        while (timer < timeToFogChange)
        {
            yield return new WaitForEndOfFrame();
            RenderSettings.fogDensity = _fogDensityChanging.Evaluate(timer/timeToFogChange);
            skybox.SetFloat("_Exposure", _skyExposureChanging.Evaluate(timer/timeToFogChange) * 0.16f);
            DynamicGI.UpdateEnvironment(); 
            //skybox.color = _skyExposureChanging.Evaluate(timer/timeToFogChange) * Color.white;
            timer += Time.deltaTime;
        }
    }

    IEnumerator DecreaseFogDensity()
    {
        float timer = 0f;
        while (timer < timeToFogChange)
        {
            yield return new WaitForEndOfFrame();
            RenderSettings.fogDensity = _fogDensityChangingOut.Evaluate(1 - timer/timeToFogChange);
            skybox.SetFloat("_Exposure", _skyExposureChanging.Evaluate(1 - timer/timeToFogChange) * 0.16f);
            DynamicGI.UpdateEnvironment(); 
            //skybox. = _skyExposureChanging.Evaluate(1 - timer/timeToFogChange) * Color.white;
            timer += Time.deltaTime;
        }
    }
}
