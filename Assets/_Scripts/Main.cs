using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [Header("Inscribed")]
    public bool screenChange = false;

    private void Awake()
    {
        if (screenChange)
            Invoke(nameof(ScreenLoader), 3f);
    }

    void ScreenLoader()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene == SceneManager.GetSceneByName("Credits"))
        {
            SceneManager.LoadScene("Menu");
        }

        SceneManager.LoadScene(scene.buildIndex + 1);
    }
}
