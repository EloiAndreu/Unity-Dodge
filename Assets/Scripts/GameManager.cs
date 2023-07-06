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
		if(!comptadorEnrrereON){
			timeText.text = "Temps: " + tempsTranscorregut.ToString("F2");
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
				Time.timeScale = 1f;
			}
		}
    }

	public void GameFinished(){
		FindObjectOfType<AudioManager>().Play("Explosion");
		Time.timeScale = 0f;

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
		tempsErrereText.color = colorTextVermell;
		tempsErrereText.text = "Has Perdut :<";
		finalText.color = colorTextVermell;
		finalText.text = tempsTranscorregut.ToString("F2");
		anuncisRestants.text = "Anuncis restants: " + anuncisRest.ToString();
        //tt.FadeInText(finalText);
		optionsMenu.SetActive(true);
		rewardedAds.LoadAd();
	}

	public void EnableDisableUI(bool enabled){
		//map.SetActive(enabled);
		pauseButon.SetActive(enabled);
		timeText.gameObject.SetActive(enabled);
	}

	public void Reward(){
		comptadorEnrere = 3f;
		buttonsObj.SetActive(false);
		comptadorEnrrereON = true;
		if(anuncisRest>0) anuncisRest--;
	}
}
