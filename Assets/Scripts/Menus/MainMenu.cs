using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int panelToStart;
    public bool isMainMenu = false;
    public Animator animFons;
    public ClassicMode classicMode;

    void Awake(){
        if(isMainMenu){
            for(int i=0; i<transform.childCount; i++){
                if(i != panelToStart) transform.GetChild(i).gameObject.SetActive(false);
                else transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public void MoveToScene(int scene){
        SetTimeScale(1);
        SceneManager.LoadScene(scene);
    }

    public void NextScene(){
        SetTimeScale(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void BeforeScene(){
        SetTimeScale(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

    public void QuitGame(){
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Restart(){
        SetTimeScale(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetTimeScale(float value){
        Time.timeScale = value;
    }

    public void Pause(){
        ///animFons.SetBool("Clar", true);
        //classicMode.AturarTOT();
        SetTimeScale(0);
    }

    public void ClickMode(){
        ModeMenu2 scipt = GameObject.FindObjectOfType<ModeMenu2>();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + scipt.imgIndex+1);
    }

    
}
