using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    public List<Button> levels;
    private void Start()
    {
        foreach(Button l in levels)
        {
            l.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        levels[0].interactable = true;
        if (BetweenScenes.level1 == true)
        {
            levels[1].interactable = true;
        }
        if (BetweenScenes.level2 == true)
        {
            levels[2].interactable = true;
        }
        if (BetweenScenes.level3 == true)
        {
            levels[3].interactable = true;
        }
    }
}
