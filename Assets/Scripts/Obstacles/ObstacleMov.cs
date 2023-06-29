using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMov : MonoBehaviour
{
    int velocitat = 0;
    Vector3 dir;

    public void StartMoving(Vector3 _dir, int _vel){
        velocitat = _vel;
        dir = _dir;
        GetComponent<Rigidbody>().AddForce(dir * velocitat * 1000);
    }

    void Update()
    {
        if(transform.position.x > 20){
            Destroy(this.gameObject);
        }
    }
}
