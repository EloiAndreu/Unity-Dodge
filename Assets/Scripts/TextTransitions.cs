using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTransitions : MonoBehaviour
{
    public float fadeDuration = 2f;

    private Coroutine currentFadeCoroutine;

    public void FadeInText(TMP_Text text)
    {
        Color startColor = text.color;
        text.color = new Color(startColor.r, startColor.g, startColor.b, 0f);

        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
        }

        currentFadeCoroutine = StartCoroutine(FadeTextCoroutine(text, 0f, 1f));
    }

    public void FadeOutText(TMP_Text text)
    {
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
        }

        currentFadeCoroutine = StartCoroutine(FadeTextCoroutine(text, 1f, 0f));
    }

    IEnumerator FadeTextCoroutine(TMP_Text tmpText, float startAlpha, float targetAlpha)
    {
        Color startColor = tmpText.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            Color newColor = new Color(startColor.r, startColor.g, startColor.b, alpha);
            tmpText.color = newColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        tmpText.color = targetColor;
    }
}
