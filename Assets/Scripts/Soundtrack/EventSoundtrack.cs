using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSoundtrack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.SwitchBackgroundMusic(Soundtracks.EVENT);
    }

    void OnDestroy()
    {
        // If the player was not reset to 3 turns
        if (Player.Instance.Turn != 3 && Player.Instance.Turn != 0){
            SoundManager.Instance.SwitchBackgroundMusic(Soundtracks.MAP);
        }
    }
}
