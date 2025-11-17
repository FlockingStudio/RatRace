using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginSoundtrack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.SwitchBackgroundMusic(Soundtracks.LOGIN);
    }
}
