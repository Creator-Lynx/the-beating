using UnityEngine;

public class MeatInteractiveBehaviour : MonoBehaviour, IInteractivable
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Collider _collider;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] float pitchShiftExtremum = 0.1f;

    void OnCollisionEnter(Collision collision)
    {
        audioSource.pitch = 1 + Random.Range(-1f, 1f) * pitchShiftExtremum;
        audioSource.Play();
    }

    public void EnableDropState()
    {
        _collider.enabled = true;
        _rigidbody.useGravity = true;
    }
}
