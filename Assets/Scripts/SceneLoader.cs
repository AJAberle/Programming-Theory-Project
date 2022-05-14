using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    private int sceneToLoad; 

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return; 
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    public void LoadScene(int index)
    {
        sceneToLoad = index; 
        SceneManager.LoadScene(1);
        StartCoroutine(WaitToLoad()); 
    }

    IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(0.1f);
        LoadSpinner.Instance.LoadScene(sceneToLoad); 
    }
}
