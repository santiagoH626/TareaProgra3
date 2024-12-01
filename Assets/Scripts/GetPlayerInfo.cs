using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
//using Newtonsoft.Json;

public class GetPlayerInfo : MonoBehaviour
{
    public string input_username;
    public int thisLevelId;
    public PlayerInfoResultModel scores;


    public TextMeshProUGUI tmpScores;
    

    void Start(){
        //ExecuteSendRequest(input_username);
        //StartCoroutine(SendRequest(input_username));
    }
    public void ExecuteSendRequest(string username)
    {
        StartCoroutine(SendRequest(username));
    }

    IEnumerator SendRequest(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/promedio3/ejercicios1.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ProtocolError ||
                www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("error");
                tmpScores.text = "Scores:\nnoone";
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                //OnCallback?.Invoke(JsonUtility.FromJson<PlayerInfoResultModel>(www.downloadHandler.text));
                scores = JsonUtility.FromJson<PlayerInfoResultModel>(www.downloadHandler.text);
                //tmpScores.text = "Scores:\n"+www.downloadHandler.text;
                ReadScores(scores,tmpScores);
            }
        }

    }
    public void ReadScores(PlayerInfoResultModel scr,TextMeshProUGUI txt){
        int n = scr.scores.Length;
        string s = "Scores:\n";
        for(int i = 0; i<n; i++){
            s+="    "+scr.scores[i].level_id.ToString()+": "+scr.scores[i].score.ToString()+"\n";
        }
        tmpScores.text = s;
    }
    public int GetScore (int lv){
        if (scores!=null){
            bool levelScoreExists = false;
            int ind = -1;
            for(int i = 0; i<scores.scores.Length;i++){
                if (scores.scores[i].level_id == lv){
                    ind = i;
                    levelScoreExists = true;
                }
            }
            if (levelScoreExists){
                return scores.scores[ind].score;
            } else {
                return 0;
            }
            
        }
        return 0;
    }
}
