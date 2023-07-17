using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeMenu : MonoBehaviour
{
    public int imgID = 1;
    public GameObject[] imatges;
    public Sprite[] sprites;
    public Animator anim1, anim2;

    void Start(){
        MostrarImgCostats(false);
        //MovDreta();
    }

    void Update(){
        if(anim1.gameObject.activeSelf && !anim1.GetCurrentAnimatorStateInfo(0).IsName("RedueixUI")){
            if (!(anim1.GetCurrentAnimatorStateInfo(0).IsName("DesapareixerEUI") || anim1.GetCurrentAnimatorStateInfo(0).IsName("DesapareixerDUI"))){
                anim1.SetInteger("AnimDespl", 0);
                MostrarImgCostats(false);
            }
        }
    }

    public void MovDreta(){
        if(imgID >= 0){
            imgID--;
            if(imgID < 0) imgID = 0;

            AssignarImatgesD();
            MostrarImgCostats(true);

            anim1.SetInteger("AnimDespl", 1); //Apareixer Esqu
            anim2.SetInteger("AnimDespl", 4); //Desapareixer Dreta
        }        
    }

    public void MovEsquerra(){
        if(imgID < sprites.Length){
            imgID++;
            if(imgID >= sprites.Length-1) imgID = sprites.Length-1;

            AssignarImatgesE();
            MostrarImgCostats(true);

            anim1.SetInteger("AnimDespl", 2); //Apareixer Esqu
            anim2.SetInteger("AnimDespl", 4); //Desapareixer Dreta
        }        
    }


    void MostrarImgCostats(bool mode){
        imatges[0].SetActive(mode);
        imatges[1].SetActive(!mode);
        imatges[2].SetActive(mode);
    }

    void AssignarImatgesD(){
        /*
        if((imgID-2)>=0 && (imgID-2)<sprites.Length) imatges[0].GetComponent<Image>().sprite = sprites[imgID-2];
        if(imgID>=0 && imgID<sprites.Length) imatges[1].GetComponent<Image>().sprite = sprites[imgID];
        if((imgID-1)>=0 && (imgID-1)<sprites.Length) imatges[2].GetComponent<Image>().sprite = sprites[imgID-1];
        */
    }

    void AssignarImatgesE(){
        /*
        if((imgID+1)>=0 && (imgID+1)<sprites.Length) imatges[0].GetComponent<Image>().sprite = sprites[imgID+1];
        if(imgID>=0 && imgID<sprites.Length) imatges[1].GetComponent<Image>().sprite = sprites[imgID];
        if((imgID+2)>=0 && (imgID+2)<sprites.Length) imatges[2].GetComponent<Image>().sprite = sprites[imgID+2];
        */
    }
}
