using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public void StartGame()
    {
        SceneLoader.Instance.LoadScene(3); 
    }
}
