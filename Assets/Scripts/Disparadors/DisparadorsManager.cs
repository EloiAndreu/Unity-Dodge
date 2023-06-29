using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparadorsManager : MonoBehaviour
{
    GameObject[] disparadors;
    public GameObject disparador;
    Vector3 positionDisparadors = new Vector3(-4.5f, 1.5f, -3f);

    void Start(){
        disparadors = GameManager.Instance.disparadors;
    }

    public void InstantiateDisparadors(int xDisparadors, int yDisparadors){
        for(int i=0; i<xDisparadors; i++){
            for(int j=0; j<yDisparadors; j++){
                Vector3 posDisp = new Vector3(positionDisparadors.x, positionDisparadors.y+j, positionDisparadors.z+i);
                Quaternion rotDisp = Quaternion.Euler(0f, 90f, 0f);
                Instantiate(disparador, posDisp, rotDisp, this.transform);
            }
        }
    }

    public void ShootRandomDisparador(int velocitat, bool isBox){
        int randomDisparador = Random.Range(0, disparadors.Length);
        disparadors[randomDisparador].GetComponent<Disparador>().Shoot(velocitat, isBox);
    }

    public void ShootFollowingPlayer(int velocitat, bool isBox){
        for(int i=0; i<disparadors.Length; i++){
            disparadors[i].GetComponent<Disparador>().ShootDetectingPlayer(velocitat, isBox);
        }
    }
}
