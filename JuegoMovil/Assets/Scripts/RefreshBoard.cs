using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshBoard : MonoBehaviour
{
  
    public void refresh(Match3GameManager GameManager)
    {
        GameManager.Start();
        GameManager.movements--;

    }
}
