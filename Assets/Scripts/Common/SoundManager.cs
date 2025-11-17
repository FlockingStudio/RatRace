using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public AudioSource buttonClickSound;
    public AudioSource diceRollSound;
    public AudioSource diceShakeSound;
    public AudioSource mailActionSound;
    public AudioSource coinSoundGain;
    public AudioSource coinSoundLose;
    public AudioSource mailSound;
    public AudioSource trophySound;
    public AudioSource MapBackground;
    public AudioSource EventBackground; // need to change this with other background track
    public AudioSource LoginBackground;
    public AudioSource DesktopBackground;
    public AudioSource trashBinSound;
    public AudioSource browserSound;
    public float volume = 1;

    private AudioSource BackgroundMusic;

    private void Awake()
    {
        Debug.Log(isActiveAndEnabled);
        if (Instance == null)
        {
            Instance = this;
            BackgroundMusic = LoginBackground;

            // Set all background music to loop
            if (MapBackground != null) MapBackground.loop = true;
            if (EventBackground != null) EventBackground.loop = true;
            if (LoginBackground != null) LoginBackground.loop = true;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize audio sources if needed
        //PlayBackgroundMusic();
    }

    private void Update()
    {
    }

    public void PlayDiceRoll()
    {
        // Implement sound playing logic here
        diceRollSound.Play();
    }

    public void PlayDiceShake()
    {
        diceShakeSound.Play();
    }

    public void PlayButtonClick()
    {
        buttonClickSound.Play();
    }

    public void PlayBackgroundMusic()
    {
        if (!BackgroundMusic.isPlaying)
        {
            BackgroundMusic.Play();
        }
    }

    public void PauseBackgroundMusic()
    {
        if (BackgroundMusic.isPlaying)
        {
            BackgroundMusic.Pause();
        }
    }
    
    public void StopBackgroundMusic()
    {
        if (BackgroundMusic.isPlaying)
        {
            BackgroundMusic.Stop();
        }
    }

    public void ResumeBackgroundMusic()
    {
        if (!BackgroundMusic.isPlaying)
        {
            BackgroundMusic.UnPause();
        }
    }

    public void PlayCoinSoundGain()
    {
        coinSoundGain.Play();
    }

    public void PlayCoinSoundLose()
    {
        coinSoundLose.Play();
    }

    public void SwitchBackgroundMusic(Soundtracks track)
    {
        StopBackgroundMusic();
        switch (track)
        {
            case Soundtracks.MAP:
                BackgroundMusic = MapBackground;
                break;
            case Soundtracks.EVENT:
                BackgroundMusic = EventBackground;
                break;
            case Soundtracks.DESKTOP:
                BackgroundMusic = DesktopBackground;
                break;
            default:
                BackgroundMusic = LoginBackground;
                break;
            
        }
        BackgroundMusic.Play();
    }
    
    public void PlayMailSound()
    {
        mailSound.Play();
    }

    public void PlayTrophySound()
    {
        trophySound.Play();
    }

    public void PlayMailActionSound()
    {
        mailActionSound.Play();
    }

    public void PlayBrowserSound()
    {
        browserSound.Play();
    }

    public void SetVolume(float newVolume)
    {
        volume = newVolume;

        if (buttonClickSound != null) buttonClickSound.volume = volume;
        if (diceRollSound != null) diceRollSound.volume = volume;
        if (diceShakeSound != null) diceShakeSound.volume = volume;
        if (coinSoundGain != null) coinSoundGain.volume = volume;
        if (coinSoundLose != null) coinSoundLose.volume = volume;
        if (MapBackground != null) MapBackground.volume = volume;
        if (EventBackground != null) EventBackground.volume = volume;
        if (LoginBackground != null) LoginBackground.volume = volume;
        if (trashBinSound != null) trashBinSound.volume = volume;
        if (browserSound != null) browserSound.volume = volume;
        if (mailSound != null) mailSound.volume = volume;
        if (DesktopBackground != null) DesktopBackground.volume = volume;
        if (mailActionSound != null) mailActionSound.volume = volume;
        if (trophySound != null) trophySound.volume = volume;
    }
}