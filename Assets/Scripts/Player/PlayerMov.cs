using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public Rigidbody rb;

    public Vector3 direccio;
    Vector3 newPosition;
    bool esMou = false;

    void Start(){
        Vector3 cantonada = new Vector3(5f, 1f, -3.5f);
        int x = GameManager.Instance.xDisparadors;
        int y = GameManager.Instance.yDisparadors;
        Vector3 initialPos = cantonada + new Vector3(0f, y/2f, x/2f);
        transform.position = initialPos;
    }

    void FixedUpdate()
    {
        //Inputs
        if (Input.GetKey(KeyCode.D) && !esMou){
            direccio = transform.right;
        }
        if (Input.GetKey(KeyCode.A) && !esMou){
            direccio = -transform.right;
        }
        if (Input.GetKey(KeyCode.W) && !esMou){
            direccio = transform.up;
        }
        if (Input.GetKey(KeyCode.S) && !esMou){
            direccio = -transform.up;
        }

        newPosition = PointToMove();
        transform.position = newPosition;
    }

    Vector3 PointToMove(){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direccio, out hit))
        {
            //Debug.Log(hit.point - direccio*0.55f);
            return hit.point - direccio*0.5f;
        }
        else{
            return transform.position;
        }
    }

}
