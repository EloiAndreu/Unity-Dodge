using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

	public Color colorTextVermell, colorTextBlanc;

	public RewardedAdsButton rewardedAds;
	bool comptadorEnrrereON = false;
	float comptadorEnrere = 3f;
	public TMP_Text tempsErrereText;
	public GameObject buttonsObj;
	public Image panelFinal;

	GameData data;
	public TMP_Text maxScore;

	public Animator animFons;
	public ClassicMode classicMode;
	
	void Awake()
	{
		Instance = this;
		LoadGame();
		dispManager.InstantiateDisparadors(xDisparadors, yDisparadors);
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Start(){
		//Time.timeScale = 1f;
        disparadors = GameObject.FindGameObjectsWithTag("Disparador");
		optionsMenu.SetActive(false);
	}

	void Update()
    {
		if(!comptadorEnrrereON){
			timeText.text = tempsTranscorregut.ToString("F2")+" s";
			if(!GameEnded) tempsTranscorregut += Time.deltaTime;
		}
		else{
			if(comptadorEnrere > 0f){
				comptadorEnrere -= Time.unscaledDeltaTime;
				
				Color color = panelFinal.color;
				color.a = 0f;
				panelFinal.color = color;

				tempsErrereText.color = colorTextBlanc;
				tempsErrereText.text = "Apunt?";
				finalText.color = colorTextBlanc;
				finalText.text = comptadorEnrere.ToString("F2");
			}
			else{
				comptadorEnrere = 0f;
			}

			if(comptadorEnrere <= 0f){
				comptadorEnrrereON = false;
				GameEnded = false;
				EnableDisableUI(true);
				optionsMenu.SetActive(false);
				Instantiate(playerPrefab, newPlayerposition, newPlayerrotation);
				animFons.SetBool("Clar", true);
				classicMode.IniciarClassicMode();
			}
		}
    }

	public void GameFinished(){
		FindObjectOfType<AudioManager>().Play("Explosion");
		//Time.timeScale = 0f;
		classicMode.AturarTOT();


		if(anuncisRest<=0) {
			recompensaButon.SetActive(false);
		}
		EnableDisableUI(false);

		Color color = panelFinal.color;
        color.a = 0.8f;
        panelFinal.color = color;

		buttonsObj.SetActive(true);
		//StopAllCoroutines();
		GameEnded = true;

		float score = tempsTranscorregut;
		if(data.maxScore < score){
			SaveGame(true, score);
			maxScore.text = "Max: " + score.ToString("F2");
		}
		else maxScore.text = "Max: " + data.maxScore.ToString("F2");

		tempsErrereText.color = colorTextVermell;
		tempsErrereText.text = "Has Perdut :<";
		finalText.color = colorTextVermell;
		finalText.text = score.ToString("F2");

		anuncisRestants.text = "Anuncis restants: " + anuncisRest.ToString();
        //tt.FadeInText(finalText);
		animFons.SetBool("Clar", false);
		optionsMenu.SetActive(true);
		rewardedAds.LoadAd();
	}

	public void EnableDisableUI(bool enabled){
		//map.SetActive(enabled);
		if(!enabled) animFons.SetBool("Clar", true); 
		pauseButon.SetActive(enabled);
		timeText.gameObject.SetActive(enabled);
	}

	public void Reward(){
		comptadorEnrere = 3f;
		buttonsObj.SetActive(false);
		comptadorEnrrereON = true;
		if(anuncisRest>0) anuncisRest--;
	}

	public void SaveGame(bool tut, float time){
		SaveSystem.SaveGame(tut, time);
	}

	public void LoadGame(){
		data = SaveSystem.LoadGame();
	}

	public void ActivarCompatdorEnrrere(){
		comptadorEnrrereON = true;
	}
}
