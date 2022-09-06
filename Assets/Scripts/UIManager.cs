using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public void Buttons(int ButtonNo)
    {
        if (ButtonNo == 1)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
            }
        }
    }
}
