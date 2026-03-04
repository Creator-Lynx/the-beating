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
    [SerializeField] float _timeToFogChange = 1f;

    [Space(30)]
    [Header("Print modification")]
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] AudioSource printSound, printSound2;
    [SerializeField] float _timeToPrintSymbol = 0.05f;
    [SerializeField] float _timeRandomRangeToPrint = 0.02f;
    [SerializeField] float _pitchRandomRange = 0.1f;
    float _defaultPrintSoundPitch = 1f;


    public bool IsPrinting = false;


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
                if(IsPrinting)
                    PrintAll();
                else
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
        while (timer < _timeToFogChange)
        {
            yield return new WaitForEndOfFrame();
            RenderSettings.fogDensity = _fogDensityChanging.Evaluate(timer/_timeToFogChange);
            skybox.SetFloat("_Exposure", _skyExposureChanging.Evaluate(timer/_timeToFogChange) * 0.16f);
            DynamicGI.UpdateEnvironment(); 
            timer += Time.deltaTime;
        }
    }

    IEnumerator DecreaseFogDensity()
    {
        float timer = 0f;
        while (timer < _timeToFogChange)
        {
            yield return new WaitForEndOfFrame();
            RenderSettings.fogDensity = _fogDensityChangingOut.Evaluate(1 - timer/_timeToFogChange);
            skybox.SetFloat("_Exposure", _skyExposureChanging.Evaluate(1 - timer/_timeToFogChange) * 0.16f);
            DynamicGI.UpdateEnvironment(); 
            timer += Time.deltaTime;
        }
    }

    string tmpString;
    public void Print(string str)
    {
        tmpString = str;
        StartCoroutine(PrintTextBySymbol(str));
    }

    public void PrintAll()
    {
        StopAllCoroutines();
        IsPrinting = false;
        textMesh.text = tmpString;
    }

    IEnumerator PrintTextBySymbol(string str)
    {
        IsPrinting = true;
        textMesh.text = "";
        _defaultPrintSoundPitch = printSound.pitch;
        for (int i = 0; i < str.Length; i++)
        {
            textMesh.text += str[i];
            if(i % 2 == 0)
            {
                printSound.pitch = _defaultPrintSoundPitch + Random.Range(-_pitchRandomRange, _pitchRandomRange);
                printSound.Play();
            }
            else
            {
                printSound2.pitch = _defaultPrintSoundPitch + Random.Range(-_pitchRandomRange, _pitchRandomRange);
                printSound2.Play();
            }
            
            float randomTime = Random.Range(- _timeRandomRangeToPrint, _timeRandomRangeToPrint);
            yield return new WaitForSeconds(_timeToPrintSymbol + randomTime);
        }
        printSound.pitch = _defaultPrintSoundPitch;
        IsPrinting = false;
    }

}
