using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FormTestView : MonoBehaviour
{
    [SerializeField] private TMP_InputField number1InputField;
    [SerializeField] private TMP_InputField number2InputField;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Button executeButton;
    //private FormTestController controller;

    private void Awake()
    {
        //controller = GetComponent<FormTestController>();
        executeButton.onClick.AddListener(Execute);
    }

    private void Execute()
    {
        /*
        controller.ExecuteSendRequest(
            int.Parse(number1InputField.text),
            int.Parse(number2InputField.text),
            OnCallback
            );
        */
    }

    private void OnCallback(PlayerInfoResultModel result)
    {
        /*
        resultText.text = "Resultado: \n";
        resultText.text += $"Suma: {result.addition}\n";
        resultText.text += $"Resta: {result.substraction}\n";
        resultText.text += $"Multiplicación: {result.multiplication}\n";
        resultText.text += $"División: {result.division}";
        */
    }

}