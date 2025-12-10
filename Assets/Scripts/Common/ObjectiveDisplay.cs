using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class ObjectiveDisplay : MonoBehaviour
{
    public static ObjectiveDisplay Instance { get; private set; }

    bool animate;
    private TextMeshProUGUI objectiveText;
    
    void Start()
    {
        objectiveText = GetComponent<TextMeshProUGUI>();
        objectiveText.text = "Objective: ?";
        objectiveText.color = Color.white;
    }

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

    public void SetObjective()
    {
        objectiveText.text = "Objective: $" + GameManager.Instance.targetMoney.ToString();
        StartCoroutine(FadeWhite());
    }

    public void SetNeutral()
    {
        objectiveText.text = "Objective: ?";
        StartCoroutine(FadeWhite());
    }

    //colour switching
    /*public Color defaultColor = Color.white;
    public Color highlight = Color.blue;
    public float lerpSpeed = 1.0f;

    private IEnumerator HighlightColour()
    {
        Color lerpedColor = defaultColor;
        float currentTime = 0.0f;

    }*/

    IEnumerator FadeWhite()
    {
        objectiveText.color = new Color(0.70588f, 0.56078f, 0.92157f);
        float fadeDuration = 0.7f;
        yield return new WaitForSeconds(0.7f);

        Color initialColor = objectiveText.color;
        Color targetColor = Color.white;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            objectiveText.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            yield return null;
        }

        objectiveText.color = Color.white;
    }

    /*void Update()
    {
        if (GameManager.Instance.objectiveVisible)
        {
            SetObjective();
        }
        else
        {
            SetNeutral();
        }
    }*/
}
