using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = AudioManager.Instance.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnVolumeChange()
    {
        if (slider == null || AudioManager.Instance == null)
            return;

        AudioManager.Instance.SetVolume(slider.value);
    }
}
