using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LoadSpinner : MonoBehaviour
{
    public static LoadSpinner Instance; 

    float rotationSpeed = 250;
    bool isLoading = true;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return; 
        }

        Instance = this; 
    }

    // Update is called once per frame
    void Update()
    {
        if (isLoading)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    public void LoadScene(int index)
    {
        Debug.Log(index); 
        StartCoroutine(LoadGame(index)); 
    }

    IEnumerator LoadGame(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex); 

        while(!operation.isDone)
        {
            yield return null;
            isLoading = true; 
        }

        isLoading = false; 
    }
}
