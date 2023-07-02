using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
	public TMP_Text roundText, timeText;
	public TextTransitions tt;

	public GameObject[] disparadors;
	public int xDisparadors, yDisparadors;
	public DisparadorsManager dispManager;

    public float tempsTranscorregut = 0;

    public bool GameEnded = false;

	public GameObject restartButton;
	
	
	void Awake()
	{
		Instance = this;
		dispManager.InstantiateDisparadors(xDisparadors, yDisparadors);
	}

	void Start(){
        disparadors = GameObject.FindGameObjectsWithTag("Disparador");
		restartButton.SetActive(false);
	}

	void Update()
    {
		timeText.text = "Time: " + tempsTranscorregut.ToString("F2");
        if(!GameEnded) tempsTranscorregut += Time.deltaTime;
    }

	public void GameFinished(){
		FindObjectOfType<AudioManager>().Play("Explosion");
		StopAllCoroutines();
		GameEnded = true;
		roundText.text = "You Lost :(";
        tt.ApearAndDisapare();
		restartButton.SetActive(true);
	}
}
