using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndPlayerMov2 : MonoBehaviour
{
    Vector2 fingerDownPosition;
    Vector2 fingerDirPosition;
    bool potMoure = true;

    public float minSwipeDistance = 20f;

    void Update(){
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
            fingerDownPosition = Input.GetTouch(0).position;
            fingerDirPosition = Input.GetTouch(0).position;
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved){
            fingerDirPosition = Input.GetTouch(0).position;
            if(potMoure) DetectSwipe();
        }

        if(Input.touchCount == 0){
            potMoure = true;
        }


    }

    void DetectSwipe()
    {
        potMoure = false;
        // Calcular la distancia del moviment en les dues direccions
        float swipeDistanceX = fingerDirPosition.x - fingerDownPosition.x;
        float swipeDistanceY = fingerDirPosition.y - fingerDownPosition.y;

        // Comprobar si la distancia del moviment es gran
        if (Mathf.Abs(swipeDistanceX) > minSwipeDistance || Mathf.Abs(swipeDistanceY) > minSwipeDistance)
        {
            // Determinar la direcció del moviment comparant-lo les distancies en X i Y
            if (Mathf.Abs(swipeDistanceX) > Mathf.Abs(swipeDistanceY))
            {
                // Desplaçament horizontal
                if (swipeDistanceX > 0)
                {
                    Move(Vector3.forward);
                }
                else
                {
                    Move(Vector3.back);
                }
            }
            else
            {
                // Desplaçament vertical
                if (swipeDistanceY > 0)
                {
                    Move(Vector3.up);
                }
                else
                {
                    Move(Vector3.down);
                }
            }
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
