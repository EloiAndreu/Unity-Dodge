using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeMenu : MonoBehaviour
{
    int mode = 1;
    public Image imgD, imgE, imgC;
    public Sprite[] sprites;
    public Animator anim1, anim2;

    void Start(){
        MovDreta();
    }

    void Update(){
        if (!anim1.IsInTransition(0) && !anim1.GetCurrentAnimatorStateInfo(0).IsName("RedueixUI")){
            anim1.SetInteger("AnimDespl", 0);
        }
    }

    public void MovDreta(){
        mode--;
        imgC.sprite = sprites[mode];
        imgE.sprite = sprites[mode];
        anim1.SetInteger("AnimDespl", 1); //Apareixer Esqu
        anim2.SetInteger("AnimDespl", 4); //Desapareixer Dreta
        
    }
}
