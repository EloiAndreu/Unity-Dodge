using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
	public TMP_Text finalText, timeText;
	public TextTransitions tt;

	public GameObject[] disparadors;
	public int xDisparadors, yDisparadors;
	public DisparadorsManager dispManager;

    public float tempsTranscorregut = 0;

    public bool GameEnded = false;

	public GameObject optionsMenu;
	public GameObject map;
	public GameObject pauseButon;

	public Color colorTextFinal;
	
	void Awake()
	{
		Instance = this;
		dispManager.InstantiateDisparadors(xDisparadors, yDisparadors);
	}

	void Start(){
		Time.timeScale = 1f;
        disparadors = GameObject.FindGameObjectsWithTag("Disparador");
		optionsMenu.SetActive(false);
	}

	void Update()
    {
		timeText.text = "Time: " + tempsTranscorregut.ToString("F2");
        if(!GameEnded) tempsTranscorregut += Time.deltaTime;
    }

	public void GameFinished(){
		FindObjectOfType<AudioManager>().Play("Explosion");
		timeText.gameObject.SetActive(false);
		EnableDisableUI(false);
		StopAllCoroutines();
		GameEnded = true;
		finalText.color = colorTextFinal;
		finalText.text = tempsTranscorregut.ToString("F2");
        tt.FadeInText(finalText);
		optionsMenu.SetActive(true);
	}

	public void EnableDisableUI(bool enabled){
		map.SetActive(enabled);
		pauseButon.SetActive(enabled);
	}
}
