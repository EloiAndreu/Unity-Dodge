using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTransitions : MonoBehaviour
{
    public TMP_Text roundText;
    public float fadeSpeed = 1.0f;
    public float delay = 2.0f;

    private Coroutine currentCoroutine;

    public void ApearAndDisapare(){
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(GradualText());
    }

    IEnumerator GradualText(){
        Color originalColor = roundText.color;
        roundText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.0f);

        while (roundText.color.a < 1.0f)
        {
            float newAlpha = roundText.color.a + fadeSpeed * Time.deltaTime;
            roundText.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
            yield return null;
        }

        yield return new WaitForSeconds(delay);

        while (roundText.color.a > 0.0f)
        {
            float newAlpha = roundText.color.a - fadeSpeed * Time.deltaTime;
            roundText.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
            yield return null;
        }
    }
}
