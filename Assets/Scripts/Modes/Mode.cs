using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mode : MonoBehaviour
{
    public ParametreAugmentarTemps[] parametres;
    public float startDelay = 3f;
    public float avançemFinsElSegon;

    [HideInInspector]
    public GameObject[] disparadors;

    void Start(){
        disparadors = GameManager.Instance.disparadors;
        IniciarMode();
    }

    void Update()
    {
        for(int i=0; i<parametres.Length; i++){
            if(parametres[i].esPotAgumentar){
                float tempsTranscorregut = GameManager.Instance.tempsTranscorregut + avançemFinsElSegon;
                //if(tempsTranscorregut >= parametres[i].segPerComençar){
                    parametres[i].valorActual = parametres[i].ObtenirAugment(tempsTranscorregut);

                    if(parametres[i].valorActual >= parametres[i].valorFinal){
                        parametres[i].esPotAgumentar = false;
                    }
                //}
            }
        }
    }

    public void IniciarMode(){
        StartCoroutine(EsperarIniciar());
    }

    public virtual IEnumerator EsperarIniciar(){
        yield return new WaitForSeconds(startDelay);
        //yield break;
    }

    public void AturarTOT(){
        StopAllCoroutines();
    }
}

[System.Serializable]
public class ParametreAugmentarTemps{
    public string name;
    public float valorInicial;
    public float valorFinal;
    public float tempsTotal;
    public float tempsAvançat;
    public float segPerComençar;
    public bool esPotAgumentar = true;
    public float valorActual;

    public ParametreAugmentarTemps(string _name, float _valInici, float _valFin, float _tTotal, float _tInici, float _segCom, bool _esPotAugm, float _valAct){
        this.name = _name;
        this.valorInicial = _valInici;
        this.valorFinal = _valFin;
        this.tempsTotal = _tTotal;
        this.tempsAvançat = _tInici;
        this.segPerComençar = _segCom;
        this.esPotAgumentar = _esPotAugm;
        this.valorActual = _valAct;
    }

    public float ObtenirAugment(float temps){
        return Mathf.Lerp(valorInicial, valorFinal, (temps-tempsAvançat)/tempsTotal);
    }
}
