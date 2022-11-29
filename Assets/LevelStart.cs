using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelStart : MonoBehaviour
{
    public GameObject pl1;
    public GameObject pl2;
    public GameObject pl3;

    void Start()
    {

        if (PlayerPrefs.GetInt("player") == 1)
        {
            pl1.SetActive(true);

        }
        else if (PlayerPrefs.GetInt("player") == 2)
        {
            pl2.SetActive(true);

        }
        else if (PlayerPrefs.GetInt("player")==3)
        {
            pl3.SetActive(true);
        }

    }
  


}
