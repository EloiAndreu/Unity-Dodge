using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomesDos2 : Mode
{
    public override IEnumerator EsperarIniciar(){
        yield return base.EsperarIniciar();

        StartCoroutine(Disparar());
        yield return null;
    }

    IEnumerator Disparar(){
        if(disparadors.Length == 0) disparadors = GameManager.Instance.disparadors;
        int disparadorID = Random.Range(0, disparadors.Length);
        disparadors[disparadorID].GetComponent<Disparador>().Shoot(1, false);
        
        yield return new WaitForSeconds(parametres[1].valorFinal-parametres[1].valorActual);
        if(!GameManager.Instance.GameEnded)StartCoroutine(Disparar());
    }

}
