using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
	public TMP_Text finalText, timeText, anuncisRestants;
	public TextTransitions tt;

	public GameObject[] disparadors;
	public int xDisparadors, yDisparadors;
	public DisparadorsManager dispManager;

    public float tempsTranscorregut = 0;

    public bool GameEnded = false;

	public GameObject optionsMenu;
	public GameObject map;
	public GameObject pauseButon;
	public GameObject recompensaButon;
	[HideInInspector]
	public GameObject player;
	public GameObject playerPrefab;
	public Vector3 newPlayerposition;
    public Quaternion newPlayerrotation;
	public int anuncisRest = 3;

	public Color colorTextFinal;

	public RewardedAdsButton rewardedAds;
	
	void Awake()
	{
		Instance = this;
		dispManager.InstantiateDisparadors(xDisparadors, yDisparadors);
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Start(){
		Time.timeScale = 1f;
        disparadors = GameObject.FindGameObjectsWithTag("Disparador");
		optionsMenu.SetActive(false);
	}

	void Update()
    {
		timeText.text = "Temps: " + tempsTranscorregut.ToString("F2");
        if(!GameEnded) tempsTranscorregut += Time.deltaTime;
    }

	public void GameFinished(){
		FindObjectOfType<AudioManager>().Play("Explosion");
		Time.timeScale = 0f;
		if(anuncisRest<=0) {
			recompensaButon.SetActive(false);
		}
		EnableDisableUI(false);
		//StopAllCoroutines();
		GameEnded = true;
		finalText.color = colorTextFinal;
		finalText.text = tempsTranscorregut.ToString("F2");
		anuncisRestants.text = "Anuncis restants: " + anuncisRest.ToString();
        //tt.FadeInText(finalText);
		optionsMenu.SetActive(true);
		rewardedAds.LoadAd();
	}

	public void EnableDisableUI(bool enabled){
		map.SetActive(enabled);
		pauseButon.SetActive(enabled);
		timeText.gameObject.SetActive(enabled);
	}

	public void Reward(){
		if(anuncisRest>0) anuncisRest--;
		GameEnded = false;
		EnableDisableUI(true);
		optionsMenu.SetActive(false);
		Instantiate(playerPrefab, newPlayerposition, newPlayerrotation);
		Time.timeScale = 1f;
	}
}
