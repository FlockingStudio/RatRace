using TMPro;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public AudioSource buttonClickSound;
    public AudioSource diceRollSound;
    public AudioSource diceShakeSound;
    public AudioSource backgroundMusic;
    public AudioSource coinSoundGain;
    public AudioSource coinSoundLose;
    public AudioClip backgroundTrack1;
    public AudioClip backgroundTrack2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }
    }

    public void PauseBackgroundMusic()
    {
        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Pause();
        }
    }

    public void ResumeBackgroundMusic()
    {
        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.UnPause();
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

    // Switches to track one
    public void playTrackOne()
    {
        if (backgroundMusic.clip != backgroundTrack1)
        {
            // Changes the sound track playing
            backgroundMusic.clip = backgroundTrack1;
            backgroundMusic.Play();
        }
    }

    // Switches to track two
    public void playTrackTwo()
    {
        if (backgroundMusic.clip != backgroundTrack2)
        {
            // Changes the sound track playing
            backgroundMusic.clip = backgroundTrack2;
            backgroundMusic.Play();
        }
    }
}
