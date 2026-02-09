using System.Collections;
using UnityEngine;

public class MusicThroughScenes : MonoBehaviour
{
    static MusicThroughScenes musicThroughScenes;
    void Awake()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        musicThroughScenes = this;
    }
    AudioSource audioSource;
    public static void DestroyMenuMusic()
    {
        musicThroughScenes?.DestroyMusic();
    }
    void DestroyMusic()
    {
        StartCoroutine(MusicDestroy());
    }

    IEnumerator MusicDestroy()
    {
        
        while (true)
        {
            audioSource.volume -= 0.05f;
            if (audioSource.volume < 0.05f) break;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }

}
