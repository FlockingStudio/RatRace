using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopSoundtrack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayMailSound();
        SoundManager.Instance.SwitchBackgroundMusic(Soundtracks.DESKTOP);
    }
}
