using UnityEditor;
using UnityEngine;

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

    void Start()
    {
        GettingTool(PlayerTools.Axe);
    }

    void Update()
    {
        
    }
}
