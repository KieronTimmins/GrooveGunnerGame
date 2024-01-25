using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitcher : MonoBehaviour
{
    // Gun GameObjects
    public GameObject countryGun;
    public GameObject classicalGun;
    public GameObject jazzGun;
    public GameObject edmGun;
    public GameObject rockGun;
    public GameObject houseGun;

    void Update()
    {
        // Check for key presses and switch guns accordingly
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchGun(countryGun);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchGun(classicalGun);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchGun(jazzGun);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchGun(edmGun);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchGun(rockGun);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SwitchGun(houseGun);
        }
    }

    void SwitchGun(GameObject selectedGun)
    {
        // Deactivate all guns
        countryGun.SetActive(false);
        classicalGun.SetActive(false);
        jazzGun.SetActive(false);
        edmGun.SetActive(false);
        rockGun.SetActive(false);
        houseGun.SetActive(false);

        // Activate the selected gun
        selectedGun.SetActive(true);
    }
}

