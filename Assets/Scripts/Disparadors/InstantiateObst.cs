using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstantiateObst : MonoBehaviour
{
    public TMP_Text roundText;
    public TextTransitions tt;
    public ShottingMode shottingMode;
    public GameObject[] disparadors;
    public int waves = 0;
    int maximObstaclesAlhora = 10;
    //int randomValorAnt = 12;

    void Awake(){
        disparadors = GameObject.FindGameObjectsWithTag("Disparador");
    }

    void Start(){
        StartCoroutine(NewWave());
    }

    void Update(){
        if(GameManager.Instance.GameEnded == true){
            StopAllCoroutines();
        }
    }

    IEnumerator NewWave(){

        //Creem una nova Wave
        waves++;
        Wave x = SetWaveParametres();
        roundText.text = "ROUND " + waves;
        tt.ApearAndDisapare();
        yield return new WaitForSeconds(3f);

        shottingMode.modeActive = true;
        if(waves < 5) shottingMode.SelectMode("ClassicMode", x);
        else shottingMode.SelectMode("ShootFollowingPlayer", x);

        while(shottingMode.modeActive != false){
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        StartCoroutine(NewWave());
    }

    Wave SetWaveParametres(){
        int obstacles = waves;
        int maxObstaclesAlhora = (int)waves/2;
        if(maxObstaclesAlhora > maximObstaclesAlhora){
            maxObstaclesAlhora = maximObstaclesAlhora;
        }
        int velocitatObstacles = (int)waves/10+1;
        return new Wave(obstacles, maxObstaclesAlhora, 1f, velocitatObstacles);
    }
}

public class Wave{
    public int obstacles;
    public int maxObstaclesAlhora;
    public float tempsEntreAparicio;
    public int velocitatObstacles;

    public Wave(int _obst, int _maxObst, float _tEntrApar, int _velObstacles){
        this.obstacles = _obst;
        this.maxObstaclesAlhora = _maxObst;
        this.tempsEntreAparicio = _tEntrApar;
        this.velocitatObstacles = _velObstacles;
    }
}
