using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicMode : MonoBehaviour
{
    public ParametreAugmentarTemps[] parametres;
    public int tirsPerApareixerCaixa = 20;

    GameObject[] disparadors;

    void Start(){
        disparadors = GameManager.Instance.disparadors;
        StartCoroutine(Disparar());
    }

    void Update()
    {
        for(int i=0; i<parametres.Length; i++){
            if(parametres[i].esPotAgumentar){
                float tempsTranscorregut = GameManager.Instance.tempsTranscorregut;
                parametres[i].valorActual = parametres[i].ObtenirAugment(tempsTranscorregut);

                if(parametres[i].valorActual >= parametres[i].valorFinal){
                    parametres[i].esPotAgumentar = false;
                }
            }
        }
    }

    IEnumerator Disparar(){
        bool isBox = false;
        int apareixCaixaValue = Random.Range(0, tirsPerApareixerCaixa);
        if(apareixCaixaValue==0) isBox = true;

        float randomValue = Random.value;
        if(randomValue <= parametres[0].valorActual/100f){
            for(int i=0; i<disparadors.Length; i++){
                disparadors[i].GetComponent<Disparador>().ShootDetectingPlayer(1, isBox);
            }
        }
        else{
            int randomDisparador = Random.Range(0, disparadors.Length);
            disparadors[randomDisparador].GetComponent<Disparador>().Shoot(1, isBox);
        }

        yield return new WaitForSeconds(1-parametres[1].valorActual);
        if(!GameManager.Instance.GameEnded)StartCoroutine(Disparar());
    }

    [System.Serializable]
    public class ParametreAugmentarTemps{
        public string name;
        public float valorInicial;
        public float valorFinal;
        public float tempsTotal;
        public float tempsInicial;
        public bool esPotAgumentar = true;
        public float valorActual;

        public ParametreAugmentarTemps(string _name, float _valInici, float _valFin, float _tTotal, float _tInici, bool _esPotAugm, float _valAct){
            this.name = _name;
            this.valorInicial = _valInici;
            this.valorFinal = _valFin;
            this.tempsTotal = _tTotal;
            this.tempsInicial = _tInici;
            this.esPotAgumentar = _esPotAugm;
            this.valorActual = _valAct;
        }

        public float ObtenirAugment(float temps){
            return Mathf.Lerp(valorInicial, valorFinal, (temps-tempsInicial)/tempsTotal);
        }
    }
}
