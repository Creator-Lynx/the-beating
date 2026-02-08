using UnityEngine;

public class MeatInteractiveBehaviour : MonoBehaviour, IInteractivable
{
    [SerializeField] AudioSource audioSource;

    void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
    }
}
