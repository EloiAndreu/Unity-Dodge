using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeMenu2 : MonoBehaviour
{
    public Sprite[] sprites;
    public GameObject imgModePrefab;

    public int imgIndex = 1;
    float imgWidth;

    // Variables per a la detecció de lliscaments
    private Vector2 touchStartPosition;
    private bool isSwiping = false;
    private float swipeThreshold = 50f;

    void Start(){
        imgWidth = Screen.width;
        float posInicial = -(imgIndex*imgWidth);

        for(int i=0; i<sprites.Length; i++){
            GameObject imgMode = Instantiate(imgModePrefab, transform.position, transform.rotation, transform);

            RectTransform rectTransform = imgMode.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector2(posInicial, 0);
            }

            Image image = imgMode.GetComponent<Image>();
            if (image != null)
            {
                image.sprite = sprites[i];
            }

            posInicial += imgWidth;
        }
    }
     
    void Update()
    {
        // Detectar lliscament en l'actualització del marcador de temps
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPosition = touch.position;
                    isSwiping = true;
                    break;
                case TouchPhase.Canceled:
                case TouchPhase.Ended:
                    isSwiping = false;
                    break;
            }

            if (isSwiping)
            {
                float swipeDelta = touch.position.x - touchStartPosition.x;

                // Determinar direcció del lliscament basada en el desplaçament horitzontal
                if (swipeDelta > swipeThreshold)
                {
                    MoureEsq();
                    isSwiping = false;
                }
                else if (swipeDelta < -swipeThreshold)
                {
                    MoureDreta();
                    isSwiping = false;
                }
            }
        }
    }

    public void MoureDreta(){
        if(imgIndex<sprites.Length-1){
            imgIndex++;
            for(int i=0; i<transform.childCount; i++){
                RectTransform rectTransform = transform.GetChild(i).GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition -= new Vector2(imgWidth, 0);
                }
                else
                {
                    Debug.LogWarning("No RectTransform component found on child object");
                }
            }
        }
    }

    public void MoureEsq(){
        if(imgIndex>0){
            imgIndex--;
            for(int i=0; i<transform.childCount; i++){
                RectTransform rectTransform = transform.GetChild(i).GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition += new Vector2(imgWidth, 0);
                }
                else
                {
                    Debug.LogWarning("No RectTransform component found on child object");
                }
            }
        }
    }
}
