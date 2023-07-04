using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    Vector3 direccio;
    Vector3 newPosition;
    bool esMou = false;

    public bool distancePoint = true;

    void Start(){
        Vector3 cantonada = new Vector3(5f, 1f, -3.5f);
        int x = GameManager.Instance.xDisparadors;
        int y = GameManager.Instance.yDisparadors;
        Vector3 initialPos = cantonada + new Vector3(0f, y/2f, x/2f);
        transform.position = initialPos;
    }

    void Update(){
        if (!distancePoint){
            if (Input.GetKeyDown(KeyCode.W)) Move(Vector3.up);
            else if (Input.GetKeyDown(KeyCode.S)) Move(Vector3.down);
            else if (Input.GetKeyDown(KeyCode.A)) Move(Vector3.back);
            else if (Input.GetKeyDown(KeyCode.D)) Move(Vector3.forward);
        }
    }

    void FixedUpdate(){
        if(distancePoint){
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
    }

    Vector3 PointToMove(){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direccio, out hit)){
            //Debug.Log(hit.point - direccio*0.55f);
            return hit.point - direccio*0.5f;
        }
        else{
            return transform.position;
        }
    }

    void Move(Vector3 dir){
        RaycastHit hit;
        if(Physics.Raycast(transform.position, dir, out hit)){
            if(hit.distance > 1f){
                transform.position += dir;
            }
        }
        else{
            transform.position += dir;
        }
    }
}
