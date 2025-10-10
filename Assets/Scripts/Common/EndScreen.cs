using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Plays the correct track for the end screen
        SoundManager.Instance.playTrackTwo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
