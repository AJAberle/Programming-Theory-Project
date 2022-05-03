using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isDoorLocked = true;
    public Material green;
    public Material red; 
    public GameObject doorWindow;
    public GameObject indicatorLight;
    public Light[] lights; 

    public void Unlock()
    {
        if(isDoorLocked)
        {
            isDoorLocked = false;
            doorWindow.SetActive(false);
            indicatorLight.GetComponent<Renderer>().material = green;

            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].color = green.color;
            }
        }
    }

    public void Lock()
    {
        if(!isDoorLocked)
        {
            isDoorLocked = true;
            doorWindow.SetActive(true);
            indicatorLight.GetComponent<Renderer>().material = red; 

            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].color = red.color; 
            }
        }
    }
}
