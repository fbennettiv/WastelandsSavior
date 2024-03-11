using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [Header("Inscribed")]
    public bool isScreen = false;

    private void Awake()
    {
        if (isScreen)
            Invoke(nameof(ScreenLoader), 3f);
    }

    void ScreenLoader()
    {
        SceneManager.LoadScene("_Gameplay");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
