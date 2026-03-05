using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class HeartBehaviour : MonoBehaviour, IInteractivable
{

    [SerializeField]Animator _animator;
    [SerializeField] AudioSource _hungrySound;
    [SerializeField] AudioMixerSnapshot defaultSnapshot, hungrySnapshot;
    int _hungryBoolName;
    bool isHungryState = false;

    [SerializeField] DialogVisualizer dialogVisualizer;
    [SerializeField] DialogHeart_Upgrading dialogHeart;

    int ConsumedMeatCount = 0;

    void Start()
    {
        _hungryBoolName = Animator.StringToHash("Hungry");
    }


    void Update()
    {
        
    }

    public void SetHungryState()
    {
        _animator.SetBool(_hungryBoolName, true);
        _hungrySound.Play();
        hungrySnapshot.TransitionTo(1f);
        isHungryState = true;
    }

    void SetUsualState()
    {
        _animator.SetBool(_hungryBoolName, false);
        _hungrySound.Stop();
        defaultSnapshot.TransitionTo(1f);
        isHungryState = false;
    }

    public void Interact()
    {
        SetUsualState();
        ConsumedMeatCount += HandResourceInventory.resourceInventory.ResourceTransfer();
        dialogVisualizer.EnterDialog();
        StartCoroutine(DelayForStartSpeaking());
    }

    public string GetInteractionHint()
    {
        return isHungryState? "кормить" : "говорить";
    }

    IEnumerator DelayForStartSpeaking()
    {
        yield return new WaitForSeconds(1f);
        dialogHeart.CallNextPhrase();
    }
}
