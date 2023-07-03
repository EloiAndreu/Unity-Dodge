using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject explosionParticle; 

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            GameObject particlesGO = Instantiate(explosionParticle, other.transform.position, Quaternion.identity);
            ParticleSystem particleSystem = particlesGO.GetComponent<ParticleSystem>();
            particleSystem.Play();

            Destroy(other.gameObject);
            GameManager.Instance.GameFinished();
        }
        else if(other.tag == "Box"){
            Destroy(transform.parent.gameObject);
        }
    }
}
