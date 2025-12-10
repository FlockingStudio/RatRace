using TMPro;
using UnityEngine;
using System.Collections;

public class DayDisplay : MonoBehaviour
{
    public static DayDisplay Instance { get; private set; }
    private TextMeshProUGUI dayText;

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        dayText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        dayText.SetText("Day " + Player.Instance.Day.ToString());
    }

    public void animateFade()
    {
        StartCoroutine(FadeWhite());
    }

    IEnumerator FadeWhite()
    {
        dayText.color = new Color(0.70588f, 0.56078f, 0.92157f);
        float fontChange = 5;
        float originalFont = dayText.fontSize;
        dayText.fontSize += fontChange;
        float fadeDuration = 2.0f;
        yield return new WaitForSeconds(0.7f);

        Color initialColor = dayText.color;
        Color targetColor = new Color(0.454902f,0.5411765f,0.6039216f);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            dayText.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            dayText.fontSize = Mathf.Lerp(dayText.fontSize,originalFont, elapsedTime / fadeDuration);
            yield return null;
        }

        dayText.color = new Color(0.454902f,0.5411765f,0.6039216f);
        dayText.fontSize = originalFont;
    }
}
