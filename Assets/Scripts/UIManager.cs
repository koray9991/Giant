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
            //if (SceneManager.GetActiveScene().buildIndex == 0)
            //{
            //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //}
            //if (SceneManager.GetActiveScene().buildIndex == 1)
            //{
            //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
            //}
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        if (ButtonNo == 2)
        {
            GameControl.instance.level++;
            PlayerPrefs.SetInt("level", GameControl.instance.level);
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
             
            }
            
        }
    }
}
