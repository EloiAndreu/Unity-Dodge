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

	public GameObject deathPanel;
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

	GameData data;
	public TMP_Text maxScore;

	public Animator animFons;

	public string mode;
	Mode[] actualMode;
	
	void Awake()
	{
		Instance = this;
		LoadGame();
		dispManager.InstantiateDisparadors(xDisparadors, yDisparadors);
		player = GameObject.FindGameObjectWithTag("Player");
		actualMode = FindObjectsOfType<Mode>();
		//nomesDos = GetComponent<NomesDos>();
	}

	void Start(){
		//Time.timeScale = 1f;
        disparadors = GameObject.FindGameObjectsWithTag("Disparador");
		deathPanel.SetActive(false);
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
				
				deathPanel.SetActive(false);
				GameObject jug = GameObject.FindGameObjectWithTag("Player");
				if(jug != null) {
					jug.GetComponent<PlayerMov>().enabled = true;
					jug.GetComponent<AndPlayerMov>().enabled = true;
				}
				
				actualMode[0].IniciarMode();
				//animFons.SetBool("Clar", true);
				/*
				if(mode == "Classic Mode"){
					classicMode.IniciarClassicMode();
				}
				else if(mode == "Nomes Dos"){
					nomesDos.IniciarClassicMode();
				}
				*/
			}
		}
    }

	public void GameFinished(){
		FindObjectOfType<AudioManager>().Play("Explosion");
		//Time.timeScale = 0f;
		/*
		if(mode == "Classic Mode"){
			classicMode.AturarTOT();
		}
		else if(mode == "Nomes Dos"){
			nomesDos.AturarTOT();
		}
		//classicMode.AturarTOT();
		*/
		actualMode[0].AturarTOT();

		EnableDisableUI(false);
		EnableFons(false);

		StartCoroutine(EsperaSegons(1.5f));
	}

	public void EnableDisableUI(bool enabled){
		//map.SetActive(enabled);
		//animFons.SetBool("Clar", enabled); 
		pauseButon.SetActive(enabled);
		timeText.gameObject.SetActive(enabled);
	}

	public void EnableFons(bool enabled){
		animFons.SetBool("Clar", enabled); 
	}


	public void Reward(){
		if(anuncisRest>0) anuncisRest--;
		if(player==null) {
			Debug.Log("nou player");

			player = Instantiate(playerPrefab, newPlayerposition, newPlayerrotation);
			player.GetComponent<PlayerMov>().enabled = false;
			player.GetComponent<AndPlayerMov>().enabled = false;
		}
		else{
			player.SetActive(true);
			player.GetComponent<PlayerMov>().enabled = false;
			player.GetComponent<AndPlayerMov>().enabled = false;
		}

		ActivarCompatdorEnrrere();
	}

	public void SaveGame(bool tut, float time){
		SaveSystem.SaveGame(tut, time);
	}

	public void LoadGame(){
		data = SaveSystem.LoadGame();
	}

	public void ActivarCompatdorEnrrere(){
		EnableFons(true);
		comptadorEnrere = 3f;
		buttonsObj.SetActive(false);
		comptadorEnrrereON = true;
	}

	IEnumerator EsperaSegons(float sec){
		Debug.Log("S'ha esperat");
		yield return new WaitForSeconds(sec);

		if(anuncisRest<=0) {
			recompensaButon.SetActive(false);
		}

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
		tempsErrereText.text = "Has Perdut";
		finalText.color = colorTextVermell;
		finalText.text = score.ToString("F2");

		anuncisRestants.text = "Anuncis restants: " + anuncisRest.ToString();
        //tt.FadeInText(finalText);
		//animFons.SetBool("Clar", false);
		deathPanel.SetActive(true);
		rewardedAds.LoadAd();
	}
}
