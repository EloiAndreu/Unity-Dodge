using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShottingMode : MonoBehaviour
{
    public GameObject[] disparadors;
    public bool modeActive = false;

    void Awake(){
        disparadors = GameObject.FindGameObjectsWithTag("Disparador");
    }

    public void SelectMode(string nameMode, Wave x){
        //StopAllCoroutines();
        if(nameMode == "ClassicMode"){
            StartCoroutine(ClassicMode(x));
        }
        else if(nameMode == "ShootFollowingPlayer"){
            StartCoroutine(ShootFollowingPlayer(x));
        }
    }

    //Disparant a posicions aleatories
    IEnumerator ClassicMode(Wave x){
        List<int> posAleatories = new List<int>();

        while(x.obstacles > 0){
            
            int randomObjectesAlhora = Random.Range(1, x.maxObstaclesAlhora+1);
            if(randomObjectesAlhora > x.obstacles){
                randomObjectesAlhora = x.obstacles;
            }

            List<int> posAlhora = new List<int>();

            for(int i=0; i<randomObjectesAlhora; i++){
                int randomValor = Random.Range(0, disparadors.Length);
                while(posAleatories.Contains(randomValor)){
                    randomValor = Random.Range(0, disparadors.Length);
                }
                posAleatories.Add(randomValor);
                posAlhora.Add(randomValor);          
            }

            for(int i=posAlhora.Count-1; i>=0; i--){ 
                disparadors[posAlhora[i]].GetComponent<Disparador>().Shoot(x.velocitatObstacles, false);
                posAlhora.Remove(posAlhora[i]);
                x.obstacles--;
            }

            yield return new WaitForSeconds(x.tempsEntreAparicio);
        }
        modeActive = false;
    }

    //Disparant només on està el jugador
    IEnumerator ShootFollowingPlayer(Wave x){
        while(x.obstacles > 0){

            for(int i=0; i<disparadors.Length; i++){
                bool haDisparat = disparadors[i].GetComponent<Disparador>().ShootDetectingPlayer(x.velocitatObstacles, false);
                if(haDisparat) x.obstacles--;
            }

            yield return new WaitForSeconds(x.tempsEntreAparicio);
        }
        modeActive = false;
    }
}
