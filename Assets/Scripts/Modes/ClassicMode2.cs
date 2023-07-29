using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicMode2 : Mode
{
    public float tempsMinCaixa = 10f, tempsMaxCaixa = 20f;
    public bool apareixCaixa = false;

    public List<GameObject> dispDetectats;

    public override IEnumerator EsperarIniciar(){
        yield return base.EsperarIniciar();

        StartCoroutine(Disparar());
        StartCoroutine(SpawnBoxes());
        yield return null;
    }

    IEnumerator Disparar(){
        bool isBox = false;
        if(apareixCaixa){
            isBox = true;
            apareixCaixa = false;
        }

        //Detectem disparadors al voltant del cercle
        float radi = parametres[3].valorFinal-parametres[3].valorActual;
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 detectionPos = new Vector3(-4.5f, player.position.y, player.position.z);
        dispDetectats = DetectarObjectesAlVoltant(detectionPos, radi);

        //Comprovem que no disparem mes objectes alhora que tirs
        int randomObjectesAlhora = (int)Random.Range(1, parametres[2].valorActual);
        if(randomObjectesAlhora > dispDetectats.Count) randomObjectesAlhora = dispDetectats.Count;
        
        //Disparem varis objectes ahora
        List<int> posAlhora = new List<int>();
        //Comprovem que no repetim disparadors
        for(int i=0; i<randomObjectesAlhora; i++){

            int randomValor = Random.Range(0, dispDetectats.Count);
            while(posAlhora.Contains(randomValor)){

                randomValor = Random.Range(0, dispDetectats.Count);
                Debug.Log("S'encalla aqui?");
            }
            posAlhora.Add(randomValor);          
        }

        //Disparem
        for(int i=posAlhora.Count-1; i>=0; i--){
            float randomValue = Random.value;
            if(randomValue <= parametres[0].valorActual/100f && dispDetectats.Count > 5){
                Debug.Log("Disparar Detectant Jugador");
                for(int j=0; j<disparadors.Length; j++){
                    disparadors[j].GetComponent<Disparador>().ShootDetectingPlayer(1, isBox);
                }
            }
            else{
                dispDetectats[posAlhora[i]].GetComponent<Disparador>().Shoot(1, isBox);
                posAlhora.Remove(posAlhora[i]);
            }
        }
        
        yield return new WaitForSeconds(parametres[1].valorFinal-parametres[1].valorActual);
        if(!GameManager.Instance.GameEnded)StartCoroutine(Disparar());
    }

    //Detectem tots els disparadors donada una posició i un radi
    List<GameObject> DetectarObjectesAlVoltant(Vector3 posicio, float radi)
    {
        //Debug.Log("prova");
        Collider[] objectes = Physics.OverlapSphere(posicio, radi);
        List<GameObject> objectesDetectats = new List<GameObject>();

        foreach (Collider collider in objectes)
        {
            if (collider.CompareTag("Disparador"))
            {
                GameObject objecteDetectat = collider.gameObject;
                objectesDetectats.Add(objecteDetectat);
            }
        }

        return objectesDetectats;
    }

    IEnumerator SpawnBoxes(){
        float tempsPerApareixerCaixa = Random.Range(tempsMinCaixa, tempsMaxCaixa);
        yield return new WaitForSeconds(tempsPerApareixerCaixa);
        apareixCaixa = true;
        StartCoroutine(SpawnBoxes());
    }
}
