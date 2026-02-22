using UnityEngine;

public class PlayerPauseSystem : MonoBehaviour
{
    static PlayerPauseSystem instance;

    [SerializeField] PlayerController _playerController;
    [SerializeField] GameObject _playerTools;
    [SerializeField] PlayerSoundsOperator _playerSoundsOperator;
    [SerializeField] GameObject gameplayCanvas;
 
    void Awake()
    {
        instance = this;
    }
    public static void Pause()
    {
        instance._playerController.enabled = false;
        instance._playerTools.SetActive(false);
        instance._playerSoundsOperator.SetWalkingSound(false);
        instance.gameplayCanvas.SetActive(false);

    }
    public static void Play()
    {
        instance._playerController.enabled = true;
        instance._playerTools.SetActive(true);
        instance.gameplayCanvas.SetActive(true);
    }
}
