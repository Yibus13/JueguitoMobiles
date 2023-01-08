using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Over : MonoBehaviour
{
    public List<Button> levels;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Button l in levels)
        {
            l.gameObject.SetActive(false);
        }
        for(int i = 0; i < 4; i++)
        {
            if (i == BetweenScenes.level)
            {
                levels[i].gameObject.SetActive(true);
            }
        }
    }

    
}
