using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestPhp : MonoBehaviour
{
    public string username;
    public PlayerModel playerModel;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        WWWForm form = new WWWForm();
        form.AddField("username",username);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/ejercicios12/ejercicios1.php",form))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError
            || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                playerModel = JsonUtility.FromJson<PlayerModel>(www.downloadHandler.text);
            }
        }
    }
}
