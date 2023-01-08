using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hyperlink : MonoBehaviour
{
    public string[] webs;

    public void search()
    {
        Application.OpenURL(webs[0]);
    }
}
