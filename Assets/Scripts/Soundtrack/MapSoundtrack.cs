using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSoundtrack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.SwitchBackgroundMusic(Soundtracks.MAP);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        SoundManager.Instance.SwitchBackgroundMusic(Soundtracks.DESKTOP);
    }
}
