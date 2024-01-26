using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ReplayGame : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void GoToSceneMain()
    {
        SceneManager.LoadScene("GrooveGunner");
    }




    public void GoToSceneMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}

