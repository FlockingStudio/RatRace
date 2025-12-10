using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading;

public class NumberFlash : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void Activate(int money)
    {
        string display;

        if (money > 0)
        {
            display = "+" + money.ToString();
            text.color = new Color(0.46f, 0.87f, 0.42f); // dark green
            text.SetText(display);
        }
        else if (money < 0)
        {
            display = money.ToString();
            text.color = new Color(0.96f, 0.41f, 0.33f); // dark red
            text.SetText(display);
        }

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float fadeDuration = 0.7f;
        yield return new WaitForSeconds(1.0f);

        Color initialColor = text.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            text.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            yield return null;
        }

        text.color = new Color(1f, 1f, 1f, 0f);
    }

    void Start()
    {
        text.color = new Color(1f, 1f, 1f, 0f);
    }
}
