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

    private AudioSource BackgroundMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            BackgroundMusic = OtherBackground;
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
            default:
                BackgroundMusic = OtherBackground;
                break;
        }
        BackgroundMusic.Play();
    }
}
