using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public AudioSource buttonClickSound;
    public AudioSource diceRollSound;
    public AudioSource diceShakeSound;
    public AudioSource coinSoundGain;
    public AudioSource coinSoundLose;
    public AudioSource MapBackground;
    public AudioSource OtherBackground; // need to change this with other background track
    public AudioSource MainMenuBackground;
    public AudioSource trashBinSound;
    public AudioSource browserSound;
    public AudioSource gigBackground;
    public float volume = 1;

    private AudioSource BackgroundMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            BackgroundMusic = MainMenuBackground;

            // Set all background music to loop
            if (MapBackground != null) MapBackground.loop = true;
            if (OtherBackground != null) OtherBackground.loop = true;
            if (MainMenuBackground != null) MainMenuBackground.loop = true;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize audio sources if needed
        PlayBackgroundMusic();
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

    public void SwitchBackgroundMusic(GameManager.Stage stage)
    {
        BackgroundMusic.Pause();
        switch (stage)
        {
            case GameManager.Stage.map:
                BackgroundMusic = MapBackground;
                break;
            case GameManager.Stage.gig:
                BackgroundMusic = gigBackground;
                break;
            default:
                BackgroundMusic = OtherBackground;
                break;
        }
        BackgroundMusic.Play();
    }

    public void PlayTrashBinSound()
    {
        trashBinSound.Play();
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
        if (OtherBackground != null) OtherBackground.volume = volume;
        if (MainMenuBackground != null) MainMenuBackground.volume = volume;
        if (trashBinSound != null) trashBinSound.volume = volume;
        if (browserSound != null) browserSound.volume = volume;
    }
}
