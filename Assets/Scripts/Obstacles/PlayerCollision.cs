using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            Destroy(other.gameObject);
            GameManager.Instance.GameFinished();
        }
        else if(other.tag == "Box"){
            Destroy(transform.parent.gameObject);
        }
    }
}
