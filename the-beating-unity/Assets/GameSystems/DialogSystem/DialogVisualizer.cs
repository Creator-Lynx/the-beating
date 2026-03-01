using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using TMPro;

public class DialogVisualizer : MonoBehaviour
{
    [SerializeField] CinemachineCamera cinemachineCamera;
    [SerializeField] LiveObjectShifterDialog cameraShifter;
    [SerializeField] Animator _dialogCanvasAnimator;
    [SerializeField] string _canvasOnTriggerName = "Show", _canvasOffTriggerName = "Hide";
    int _canvasOnTrigger, _canvasOffTrigger;

    [SerializeField] AnimationCurve _fogDensityChanging;
    [SerializeField] AnimationCurve _fogDensityChangingOut;
    [SerializeField] AnimationCurve _skyExposureChanging;
    [SerializeField] Material skybox;
    [SerializeField] float timeToFogChange = 1f;

    [Space(30)]
    [Header("Print modification")]
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] float timeToPrintSymbol = 0.05f;
    [SerializeField] float timeRandomRangeToPrint = 0.02f;


    void Start()
    {
        _canvasOnTrigger = Animator.StringToHash(_canvasOnTriggerName);
        _canvasOffTrigger = Animator.StringToHash(_canvasOffTriggerName);
    }

    public bool dialogState = false;
    public void EnterDialog()
    {
        cinemachineCamera.enabled = true;
        cameraShifter.enabled = true;
        _dialogCanvasAnimator.SetTrigger(_canvasOnTrigger);
        
        StartCoroutine(DialogStateDelay());
        

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        StartCoroutine(IncreaseFogDensity());
    }

    public void ExitDialog()
    {
        cinemachineCamera.enabled = false;
        cameraShifter.enabled = false;
        _dialogCanvasAnimator.SetTrigger(_canvasOffTrigger);

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
            if (UnityEngine.InputSystem.Keyboard.current.eKey.wasPressedThisFrame) 
                ExitDialog();
        if (dialogState) 
            if (UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame) 
                Print("тестовая строка тестовая строка тестовая строка тестовая строка тестовая строка ");
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

    public void Print(string str)
    {
        StartCoroutine(PrintTextBySymbol(str));
    }

    IEnumerator PrintTextBySymbol(string str)
    {
        textMesh.text = "";
        for (int i = 0; i < str.Length; i++)
        {
            textMesh.text += str[i];
            //audiosource
            float randomTime = Random.Range(- timeRandomRangeToPrint, timeRandomRangeToPrint);
            Debug.Log(randomTime);
            yield return new WaitForSeconds(timeToPrintSymbol + randomTime);
        }
    }

}
