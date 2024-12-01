using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    public string input_username;
    public TMP_InputField playerInputField;
    public Button savePlayerButton;


    public int input_level;
    public TMP_Dropdown levelDropdown;
    public Button playLevelButton;
    public GameObject playButton;

    public GetPlayerInfo getInfo;
    public GameInfo gameInfo;

    void Start()
    {
        savePlayerButton.onClick.AddListener(OnSaveButtonClick);

        levelDropdown.onValueChanged.AddListener(OnLevelSelected);
        OnLevelSelected(levelDropdown.value);
    
        playLevelButton.onClick.AddListener(OnPlayButtonClick);
        playButton.SetActive(false);

        GameObject gameInfoObject = GameObject.FindWithTag("Global");
        gameInfo = gameInfoObject.GetComponent<GameInfo>();
    }

    // Método que guarda el texto del InputField en la variable
    void OnSaveButtonClick()
    {
        input_username = playerInputField.text;

        getInfo.ExecuteSendRequest(input_username);
        playButton.SetActive(true);
    }
    void OnLevelSelected(int index)
    {
        input_level = index + 1;
    }

    void OnPlayButtonClick()
    {
        gameInfo.username = input_username;
        gameInfo.level = input_level;
        gameInfo.score = getInfo.GetScore(input_level);
        SceneManager.LoadScene("Level"+input_level.ToString());

        //cambiar escena
        //input_level = inputField.text;

        //getInfo.ExecuteSendRequest(input_username);
    }

}
