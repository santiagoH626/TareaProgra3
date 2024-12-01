using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInfoResultModel
{
    public Score[] scores;
}
[Serializable]
public class Score{
    public int level_id;
    public int score; 
}