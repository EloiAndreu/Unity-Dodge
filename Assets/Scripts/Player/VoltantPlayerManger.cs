using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltantPlayerManger : MonoBehaviour
{
    int midaX, midaY;
    public GameObject cubPrefab;

    void Start(){
        midaX = GameManager.Instance.xDisparadors;
        midaY = GameManager.Instance.yDisparadors;
        InstantiateVoltantPlayer();
    }

    void InstantiateVoltantPlayer(){
        //Dalt/Baix
        Vector3 posBaix = new Vector3(5f, 0.5f, -4.5f+midaX/2f+1f);
        GameObject cubBaix = Instantiate(cubPrefab, posBaix, Quaternion.identity, this.transform);
        cubBaix.transform.localScale = new Vector3(1f, 1f, midaX);

        Vector3 posDalt = new Vector3(5f, 0.5f+midaY+1, -4.5f+midaX/2f+1f);
        GameObject cubDalt = Instantiate(cubPrefab, posDalt, Quaternion.identity, this.transform);
        cubDalt.transform.localScale = new Vector3(1f, 1f, midaX);

        //Dreta/Esq
        Vector3 posEsq = new Vector3(5f, 1f+midaY/2f, -4f);
        GameObject cubEsq = Instantiate(cubPrefab, posEsq, Quaternion.identity, this.transform);
        cubEsq.transform.localScale = new Vector3(1f, midaY+2, 1f);

        Vector3 posDreta = new Vector3(5f, 1f+midaY/2f, -4f+midaX+1);
        GameObject cubDreta = Instantiate(cubPrefab, posDreta, Quaternion.identity, this.transform);
        cubDreta.transform.localScale = new Vector3(1f, midaY+2, 1f);
    }


}
