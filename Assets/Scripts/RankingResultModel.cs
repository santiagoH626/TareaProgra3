using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RankingResultModel
{
    public RankingScore[] scores;
}

[Serializable]
public class RankingScore
{
    public string username;
    public int score;
}
