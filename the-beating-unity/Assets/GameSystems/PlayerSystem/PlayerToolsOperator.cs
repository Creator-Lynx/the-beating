using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerToolsOperator : MonoBehaviour
{
    //analog interfase to get acces to tool
    public enum PlayerTools
    {
        Axe,
        Gun,
        Shotgun
    }

    //have-states
    bool[] haveTools = {false, false, false};


    //tool gameobgects
    [SerializeField] GameObject[] tools;

    void CallTool(PlayerTools index)
    {
        if (!haveTools[(int)index]) return; // do nothing if player dont have tools

        for (int i = 0; i < tools.Length; i++)
        {
            tools[i]?.gameObject.SetActive(false);
            //when I will got interface for tools I can do the hiding animations by calling hide method
        }

        tools[(int)index].SetActive(true);
        //when I will got interface for tools I can do the showing animations by calling show method
    }

    public void GettingTool(PlayerTools index)
    {
        int i = (int)index;
        haveTools[i] = true;
        CallTool(index);
    }


    //operate inputs
    InputAction selectTool1;
    InputAction selectTool2;
    InputAction selectTool3;
    InputAction selectTool4;

    void Awake()
    {
        selectTool1 = InputSystem.actions.FindAction("FirstTool");
        selectTool2 = InputSystem.actions.FindAction("SecondTool");
        selectTool3 = InputSystem.actions.FindAction("ThirdTool");
        selectTool4 = InputSystem.actions.FindAction("FourthTool");
    }
    void Update()
    {
        //test getting axe
        if(Keyboard.current.xKey.wasPressedThisFrame) GettingTool(PlayerTools.Axe);

        //input operate
        if(selectTool1.WasPressedThisFrame()) CallTool(PlayerTools.Axe);
        if(selectTool2.WasPressedThisFrame()) CallTool(PlayerTools.Gun);
        if(selectTool3.WasPressedThisFrame()) CallTool(PlayerTools.Shotgun);
        //if(selectTool4.WasPressedThisFrame()) Debug.Log("FourthTool");
    }
}
