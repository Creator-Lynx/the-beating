using UnityEngine;

public class DialogHeart_Upgrading : MonoBehaviour
{
    [SerializeField] DialogVisualizer dialogVisualizer_Heart;

    [SerializeField]
    string[] standardPhrases;

    public void CallNextPhrase()
    {
        int phraseNumber = Random.Range(0, 3);
        dialogVisualizer_Heart.Print(standardPhrases[phraseNumber]);
    }

    void Update()
    {
        if(dialogVisualizer_Heart.dialogState)
        {
            if (UnityEngine.InputSystem.Keyboard.current.anyKey.wasPressedThisFrame) 
                if(dialogVisualizer_Heart.IsPrinting)
                    dialogVisualizer_Heart.PrintAll();
                else
                {
                    dialogVisualizer_Heart.ExitDialog();
                }
                    
        }
    }


}
