using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RefreshBoard : MonoBehaviour
{
    public Match3GameManager manager;
    public void refresh()
    {
        for(int i = 0; i < manager.sizeX; i++)
        {
            for(int j=0; j < manager.sizeY * 2; j++)
            {
                if (manager.Board[i, j])
                {
                    Destroy(manager.Board[i, j].gameObject);
                    manager.Board[i, j] = null;
                }
            }
        }
        manager.Start();
        manager.movements--;
        if (manager.movements <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
