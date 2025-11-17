using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    // Sound Effects
    private AudioClip coinGain;
    private AudioClip coinLose;
    private AudioClip diceRoll;
    private AudioClip browserClick;
    private AudioClip windowClose;
    private AudioClip windowOpen;
    private AudioClip login;
    private AudioClip diceShake;
    private AudioClip mail;
    private AudioClip alert;
    private AudioClip mouseClick;

    // Background Music (OST)
    private AudioClip mapMusic;
    private AudioClip upBeatMusic;
    private AudioClip mainMusic;
    private AudioClip sadSongMusic;
    private AudioClip gigMusic;
    private AudioClip badEndMusic;

    // AudioSource for playing sounds
    private AudioSource sfxSource;
    private AudioSource musicSource;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Create AudioSource components
        sfxSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
    }

    void Start()
    {
        // Load Sound Effects from Resources
        coinGain = Resources.Load<AudioClip>("Audio/CoinGain");
        coinLose = Resources.Load<AudioClip>("Audio/CoinLose");
        diceRoll = Resources.Load<AudioClip>("Audio/DiceRoll");
        browserClick = Resources.Load<AudioClip>("Audio/browserClick");
        windowClose = Resources.Load<AudioClip>("Audio/WindowClose");
        windowOpen = Resources.Load<AudioClip>("Audio/WindowOpen");
        login = Resources.Load<AudioClip>("Audio/Login");
        diceShake = Resources.Load<AudioClip>("Audio/DiceShake");
        mail = Resources.Load<AudioClip>("Audio/Mail");
        alert = Resources.Load<AudioClip>("Audio/Alert");
        mouseClick = Resources.Load<AudioClip>("Audio/MouseClick");

        // Load Background Music (OST) from Resources
        mapMusic = Resources.Load<AudioClip>("Audio/OST/Map");
        upBeatMusic = Resources.Load<AudioClip>("Audio/OST/UpBeat");
        mainMusic = Resources.Load<AudioClip>("Audio/OST/Main");
        sadSongMusic = Resources.Load<AudioClip>("Audio/OST/SadSong");
        gigMusic = Resources.Load<AudioClip>("Audio/OST/Gig");
        badEndMusic = Resources.Load<AudioClip>("Audio/OST/BadEnd");

        PlayMainMusic();
    }

    void Update()
    {
        // Play mouse click sound on left mouse button down
        if (Input.GetMouseButtonDown(0))
        {
            PlayMouseClick();
        }
    }

    // Sound Effect Methods
    public void PlayCoinGain()
    {
        sfxSource.PlayOneShot(coinGain);
    }

    public void PlayCoinLose()
    {
        sfxSource.PlayOneShot(coinLose);
    }

    public void PlayDiceRoll()
    {
        sfxSource.PlayOneShot(diceRoll);
    }

    public void PlayBrowserClick()
    {
        sfxSource.PlayOneShot(browserClick);
    }

    public void PlayWindowClose()
    {
        sfxSource.PlayOneShot(windowClose);
    }

    public void PlayWindowOpen()
    {
        sfxSource.PlayOneShot(windowOpen);
    }

    public void PlayLogin()
    {
        sfxSource.PlayOneShot(login);
    }

    public void PlayDiceShake()
    {
        sfxSource.PlayOneShot(diceShake);
    }

    public void PlayMail()
    {
        sfxSource.PlayOneShot(mail);
    }

    public void PlayAlert()
    {
        sfxSource.PlayOneShot(alert);
    }

    public void PlayMouseClick()
    {
        sfxSource.PlayOneShot(mouseClick);
    }

    // Background Music Methods
    public void PlayMapMusic()
    {
        musicSource.clip = mapMusic;
        musicSource.Play();
    }

    public void PlayUpBeatMusic()
    {
        musicSource.clip = upBeatMusic;
        musicSource.Play();
    }

    public void PlayMainMusic()
    {
        musicSource.clip = mainMusic;
        musicSource.Play();
    }

    public void PlaySadSongMusic()
    {
        musicSource.clip = sadSongMusic;
        musicSource.Play();
    }

    public void PlayGigMusic()
    {
        musicSource.clip = gigMusic;
        musicSource.Play();
    }

    public void PlayBadEndMusic()
    {
        musicSource.clip = badEndMusic;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}