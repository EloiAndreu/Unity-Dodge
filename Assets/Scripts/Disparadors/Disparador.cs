using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparador : MonoBehaviour
{
    GameObject obstaclesParent;
    public GameObject obstaclePrefab;
    public GameObject boxPrefab;
    public float tempsEntreCanviDeColor = 0.5f;
    public Material[] materials;
    public Material[] boxMats;

    Renderer render;
    public bool disparant = false;
    Coroutine myCoroutine;
    bool primerCop = false;
    bool hiHaBox = false;

    void Awake(){
        render = GetComponent<Renderer>();
        obstaclesParent = GameObject.FindGameObjectWithTag("ObstaclesParent");
    }

    void Update(){
        //if(GameManager.Instance.GameEnded){
            //StopAllCoroutines();
        //}

        ComprovarBox();
    }

    void ComprovarBox(){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.CompareTag("Box")){
                primerCop = true;
                hiHaBox = true;
            }
            else hiHaBox = false;

        }
        else if(primerCop){
            primerCop = false;
            SetColorGreen();
        }
    }

    void Start(){
        CanviarColor(materials[0]);
        //StartCoroutine(Disparar(1));
    }

    public void Shoot(int velocitat, bool isBox){
        if(!hiHaBox){
            StartCoroutine(WaitForCoroutineThenShoot(velocitat, isBox));
        }
    }

    IEnumerator WaitForCoroutineThenShoot(int velocitat, bool isBox){
        if (myCoroutine != null){
            yield return new WaitUntil(() => myCoroutine == null);
        }

        disparant = true;
        myCoroutine = StartCoroutine(Disparar(velocitat, isBox));
    }

    IEnumerator Disparar(int velocitat, bool isBox){
        while(disparant){
            for (int i = 1; i < materials.Length; i++)
            {
                if(!isBox)CanviarColor(materials[i]);
                else CanviarColor(boxMats[i]);
                yield return new WaitForSeconds(tempsEntreCanviDeColor);
                
            }

            FindObjectOfType<AudioManager>().Play("Shoot");
            
            GameObject obstacle;
            if(isBox) obstacle = Instantiate(boxPrefab, transform.position, Quaternion.identity, obstaclesParent.transform);
            else obstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity, obstaclesParent.transform);
            obstacle.GetComponent<ObstacleMov>().StartMoving(transform.forward, velocitat);
            //obstacle.GetComponent<Rigidbody>().AddForce(transform.forward * velocitat * 1000);
            if(!isBox) CanviarColor(materials[0]);
            else CanviarColor(boxMats[2]);
            disparant = false;
        }
        myCoroutine = null;
    }

    void CanviarColor(Material mat){
        transform.GetChild(0).GetComponent<Light>().color = mat.color;
        render.materials[1] = mat;
    }

    public bool ShootDetectingPlayer(int velocitat, bool isBox){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                bool haDisparat;
                if(isBox) haDisparat = ShootBox(velocitat);
                else {
                    haDisparat = true;
                    Shoot(velocitat, false);
                }
                return haDisparat;
            }
        }
        return false;
    }

    public bool ShootBox(int velocitat){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (!hit.collider.CompareTag("Box") && !hit.collider.CompareTag("Player"))
            {
                Shoot(velocitat, true);
                return true;
            }
        }
        return false;
    }

    public void SetColorGreen(){
        //Debug.Log("Color verd");
        CanviarColor(materials[0]);
    }
}
