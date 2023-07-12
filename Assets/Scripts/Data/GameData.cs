using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData{

    public bool haFetTutorial;
    public float maxScore;

    public GameData(bool _tutorial, float _maxScore){
        haFetTutorial = _tutorial;
        maxScore = _maxScore;
    }
    
}
