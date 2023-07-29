using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomesDos : MonoBehaviour
{
    public ParametreAugmentarTemps[] parametres;
    public float startDelay = 3f;
    public float avançemFinsElSegon;

    public List<GameObject> dispDetectats;

    GameObject[] disparadors;

    void Start(){
        disparadors = GameManager.Instance.disparadors;
        StartClassicMode();
    }

    public void StartClassicMode(){
        //GameManager.Instance.tempsTranscorregut = 0f;
        StartCoroutine(StartDelay());
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

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay); // Esperar 5 segons

        StartCoroutine(Disparar());
    }

    IEnumerator Disparar(){
        float radi = parametres[3].valorFinal-parametres[3].valorActual;
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 detectionPos = new Vector3(-4.5f, player.position.y, player.position.z);
        dispDetectats = DetectarObjectesAlVoltant(detectionPos, radi);


        int randomObjectesAlhora = (int)Random.Range(1, parametres[2].valorActual);
        if(randomObjectesAlhora > dispDetectats.Count) randomObjectesAlhora = dispDetectats.Count;
        //Debug.Log(randomObjectesAlhora);
        List<int> posAlhora = new List<int>();

        for(int i=0; i<randomObjectesAlhora; i++){

            int randomValor = Random.Range(0, dispDetectats.Count);
            while(posAlhora.Contains(randomValor)){
                randomValor = Random.Range(0, dispDetectats.Count);
                Debug.Log("S'encalla aqui?");
            }
            posAlhora.Add(randomValor);          
        }

        for(int i=posAlhora.Count-1; i>=0; i--){
            float randomValue = Random.value;
            if(randomValue <= parametres[0].valorActual/100f && dispDetectats.Count > 5){
                Debug.Log("Disparar Detectant Jugador");
                for(int j=0; j<disparadors.Length; j++){
                    disparadors[j].GetComponent<Disparador>().ShootDetectingPlayer(1, false);
                }
            }
            else{
                dispDetectats[posAlhora[i]].GetComponent<Disparador>().Shoot(1, false);
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

    public void AturarTOT(){
        StopAllCoroutines();
    }

    public void IniciarClassicMode(){
        StartClassicMode();
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
}
