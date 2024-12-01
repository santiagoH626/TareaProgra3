using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class SendInfo : MonoBehaviour
{
    public string _username;
    public int _level_id;
    public int _score;
    public int _newScore;
    public GameObject global;
    public GameInfo gameInfo;
    public GameObject newRecord;


    public GameObject screen;
    public TextMeshProUGUI tmpRanking;
    // Start is called before the first frame update
    void Start()
    {
        global = GameObject.FindWithTag("Global");
        gameInfo = global.GetComponent<GameInfo>();
        _username = gameInfo.username;
        screen.SetActive(false);
        
    }

    public void EndGame(int newScore){
        gameInfo.EndGame(this);
        if (newScore>_score+1){
            _newScore = newScore;
            ExecuteSendScoreRequest();
            newRecord.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExecuteSendScoreRequest(){
        StartCoroutine(SendScoreRequest(_username,_level_id,_newScore));
    }
    IEnumerator SendScoreRequest(string username, int level_id, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("level_id", level_id);
        form.AddField("score", score);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/promedio3/ejercicios2.php", form))
        {
            yield return www.SendWebRequest();
            /*
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
            */
        }
        GetRanking();

    }
    public void GetRanking(){
        StartCoroutine(RankingRequest());
    }

    IEnumerator RankingRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("level_id", _level_id);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/promedio3/ejercicios3.php", form))
        {
            yield return www.SendWebRequest();
            
            if (www.result == UnityWebRequest.Result.ProtocolError ||
                www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("error");
                //tmpScores.text = "Scores:\nnoone";
            }
            else
            {

                Debug.Log(www.downloadHandler.text);
                //OnCallback?.Invoke(JsonUtility.FromJson<PlayerInfoResultModel>(www.downloadHandler.text));
                RankingResultModel ranking = JsonUtility.FromJson<RankingResultModel>(www.downloadHandler.text);

                yield return new WaitForSeconds(0.5f);

                screen.SetActive(true);
                ReadRanking(ranking, tmpRanking);
            }
            
        }

    }

    public void ReadRanking(RankingResultModel ranking, TextMeshProUGUI tmp){
        string s = "Ranking:\n";
        for(int i = 0; i<ranking.scores.Length; i++){
            int puesto = i+1;
            s+=puesto.ToString()+".- "+ranking.scores[i].score+" - "+ranking.scores[i].username+"\n";
        }

        tmp.text = s;
    }
}
