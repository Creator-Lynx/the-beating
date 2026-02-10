using UnityEngine;
using TMPro;

public class InteractionHint : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Animator animator;
    public void ShowHint (string str)
    {
        text.text = str + " (E)";
        animator.SetBool("isShowed", true);
    }

    public void HideHint()
    {
        animator.SetBool("isShowed", false);
    }
}
