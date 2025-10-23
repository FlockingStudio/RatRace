using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = FindObjectOfType<Slider>();
        if (SoundManager.Instance != null && slider != null)
        {
            slider.value = SoundManager.Instance.volume;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateVolume()
    {
        if (SoundManager.Instance != null && slider != null)
        {
            SoundManager.Instance.SetVolume(slider.value);
        }
    }
    
}
