using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : ObstacleMov
{
    GameObject player;

    Vector3 initialScale;
    Vector3 finalScale = Vector3.one;

    Vector3 initialPos;
    float diffPos;

    public float compteEnrereDestroy = 20f;

    void Start(){
        initialPos = transform.position;
        initialScale = transform.localScale;
        diffPos = initialPos.x - 5f;
        StartCoroutine(DestroyDelay());
    }

    void Update(){
        if(!GameManager.Instance.GameEnded && transform.position.x >= 5f){
            GetComponent<Rigidbody>().AddForce(Vector3.zero);
            transform.position = new Vector3(5f, transform.position.y, transform.position.z);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            transform.localScale = finalScale;
        }
        else if(!GameManager.Instance.GameEnded){
            float t = (transform.position.x-initialPos.x)/-diffPos;
            //Debug.Log(t);
            transform.localScale = Vector3.Lerp(initialScale, finalScale, t);
        }

        //if(GameManager.Instance.GameEnded){
            //Destroy(this.gameObject);
        //}
    }

    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(compteEnrereDestroy);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.right, out hit))
        {
            if (hit.collider.CompareTag("Disparador"))
            {
                hit.transform.GetComponent<Disparador>().SetColorGreen();
            }
        }
        Destroy(this.gameObject);
    }
}
