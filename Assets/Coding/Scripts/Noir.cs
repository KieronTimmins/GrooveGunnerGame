using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
    
using UnityEngine.Rendering.PostProcessing;
using System.Linq;
[RequireComponent(typeof(PostProcessVolume))]
public class Noir : MonoBehaviour
{
    


    


    [SerializeField] private Color defaultLightColour;
    [SerializeField] private Color boostedLightColour;

    private bool isnoirEnabled;

    private PostProcessVolume volume;

    public AudioSource audioSource;
    public AudioClip initiate;
    
    

    

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.ambientLight = defaultLightColour;

        volume = gameObject.GetComponent<PostProcessVolume>();
        volume.weight = 0;

       
    }
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Keypad2))

        {
            ToggleBlackandWhite();
        }
        

    }
   

    // Update is called once per frame
    
    private void ToggleBlackandWhite()
    {
        isnoirEnabled = !isnoirEnabled;

        if(isnoirEnabled)
        {
            RenderSettings.ambientLight = boostedLightColour;
            volume.weight = 1;
            audioSource.PlayOneShot(initiate);

        }
        else
        {
            RenderSettings.ambientLight = defaultLightColour;
            volume.weight = 0;
            
        }



    }
    


}