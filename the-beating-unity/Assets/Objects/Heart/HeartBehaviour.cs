using UnityEngine;
using UnityEngine.Audio;

public class HeartBehaviour : MonoBehaviour
{

    [SerializeField]Animator _animator;
    [SerializeField] AudioSource _hungrySound;
    [SerializeField] AudioMixerSnapshot defaultSnapshot, hungrySnapshot;
    int _hungryBoolName;

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
    }

    void SetUsualState()
    {
        _animator.SetBool(_hungryBoolName, false);
        _hungrySound.Stop();
        defaultSnapshot.TransitionTo(1f);
    }
}
