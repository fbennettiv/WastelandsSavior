using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimer : MonoBehaviour
{
    [Header("Inscribed")]
    public bool screenChanger = false;
    public float timeToWait = 3f;
    public SceneType sceneType;
    public int nextSceneInt;
    public string nextSceneString;

    public enum SceneType
    {
        Number,
        String
    }

    void Start()
    {
        if (!screenChanger)
            return;

        Invoke(nameof(NextScene), timeToWait); 
    }

    void Update()
    {
        if (screenChanger && Input.anyKeyDown)
        {
            NextScene();
        }
    }

    private void NextScene()
    {
        if (sceneType == SceneType.Number)
        {
            SceneManager.LoadScene(nextSceneInt);
        }
        else
        {
            SceneManager.LoadScene(nextSceneString);
        }
    }
}
