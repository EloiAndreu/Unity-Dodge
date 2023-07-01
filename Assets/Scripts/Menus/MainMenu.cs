using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int panelToStart;

    void Awake(){
        for(int i=0; i<transform.childCount; i++){
            if(i != panelToStart) transform.GetChild(i).gameObject.SetActive(false);
            else transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void NextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void BeforeScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

    public void QuitGame(){
        Debug.Log("Quit");
        Application.Quit();
    }

    
}
