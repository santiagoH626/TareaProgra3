using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolverIntro : MonoBehaviour
{
    public Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        
        exitButton.onClick.AddListener(ExitButtonClick);
    }

    void ExitButtonClick()
    {
        SceneManager.LoadScene("IntroScene");
    }
}
