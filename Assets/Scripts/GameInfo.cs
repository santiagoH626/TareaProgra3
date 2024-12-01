using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public string username;
    public int level;
    public int score;
    public GameObject webObject;
    public SendInfo sendInfo;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Global");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    public void EndGame(SendInfo _sendInfo){
        sendInfo = _sendInfo;
            sendInfo._username = username;
            sendInfo._level_id = level;
            sendInfo._score = score;
    }
}
