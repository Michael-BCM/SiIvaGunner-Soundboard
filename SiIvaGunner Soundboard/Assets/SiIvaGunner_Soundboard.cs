using UnityEngine;
using UnityEngine.UI;
using SiIvaGunner_Soundboard_Namespace;

public class SiIvaGunner_Soundboard : MonoBehaviour
{
    public AudioSource audioSource { get; private set; }

    public bool RandomAudioPlayedOnce { get; private set; }

    public bool isScreamButtonEnabled { get; private set; }

    private void ToggleScreamButton() { isScreamButtonEnabled = !isScreamButtonEnabled; }
            
    public bool SettingsActive { get; private set; }

    private void ToggleSettingsActive() { SettingsActive = !SettingsActive; }

    public bool AllAtOnce { get; private set; }

    private void ToggleAllAtOnce() { AllAtOnce = !AllAtOnce; }
    
    [SerializeField]
    private AudioClip LoudestNigra;

    [SerializeField]
    private Text AAOB_Text;
    
    [SerializeField]
    private AudioButton[] audioButtons;

    [SerializeField]
    private Button resetButton;

    [SerializeField]
    private Button SettingsButton;

    [SerializeField]
    private Button AllAtOnceButton;

    [SerializeField]
    private Button EnableScreamButtonButton;

    [SerializeField]
    private Button LoudNigraButton;

    [SerializeField]
    private SwapMaterialOnPress Nozoomy;

    [SerializeField]
    private SwapMaterialOnPress YazawaNiko;
   
    [SerializeField]
    private Material SiivaAvatar;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        RandomAudioPlayedOnce = isScreamButtonEnabled = false;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        for (int i = 0; i < audioButtons.Length; i++)
        {
            audioButtons[i].SetDefaultImage();
            audioButtons[i].SetWidthAndHeight();
        }

        SetButtons();

        //A click is defined as a push AND a release of the button. The method below plays on release.

        //You think you know what you're doing, don't you? Go ahead, try and turn this into a loop. See what happens. 
        audioButtons[0].button.onClick.AddListener(delegate
        { PlayAudioWithRandomAudioChance(audioButtons[0], LoudestNigra, 30, 29); });
        audioButtons[1].button.onClick.AddListener(delegate
        { PlayAudioWithRandomAudioChance(audioButtons[1], LoudestNigra, 30, 29); });
        audioButtons[2].button.onClick.AddListener(delegate
        { PlayAudioWithRandomAudioChance(audioButtons[2], LoudestNigra, 30, 29); });
        audioButtons[3].button.onClick.AddListener(delegate
        { PlayAudioWithRandomAudioChance(audioButtons[3], LoudestNigra, 30, 29); });
        audioButtons[4].button.onClick.AddListener(delegate
        { PlayAudioWithRandomAudioChance(audioButtons[4], LoudestNigra, 30, 29); });
        audioButtons[5].button.onClick.AddListener(delegate
        { PlayAudioWithRandomAudioChance(audioButtons[5], LoudestNigra, 30, 29); });
        audioButtons[6].button.onClick.AddListener(delegate
        { PlayAudioWithRandomAudioChance(audioButtons[6], LoudestNigra, 30, 29); });        
        //int times_attempted_unsuccessfully = 3;

        audioButtons[7].button.onClick.AddListener(DoTheNicoDance);
        audioButtons[8].button.onClick.AddListener(GoOnAdventure);
        audioButtons[9].button.onClick.AddListener(delegate
        { PlayAudio(audioButtons[9]); });

        EnableScreamButtonButton.onClick.AddListener(delegate { ToggleScreamButton(); });
        resetButton.onClick.AddListener(SetButtons);
        AllAtOnceButton.onClick.AddListener(delegate { ToggleAllAtOnce(); });
        SettingsButton.onClick.AddListener(delegate { ToggleSettingsActive(); });
    }

    private bool PlayAudio(AudioButton audBtn)
    {
        if ((!AllAtOnce && !audioSource.isPlaying) || AllAtOnce)
        {
            audBtn.Click();
            audioSource.PlayOneShot(audBtn.audioClip);
            return true;
        }
        return false;
    }

    private bool PlayAudioWithRandomAudioChance(AudioButton audBtn, AudioClip randomAudio, int randNum, int randThreshold)
    {
        if ((!AllAtOnce && !audioSource.isPlaying) || AllAtOnce)
        {
            audBtn.Click();
            if (Random.Range(0, randNum) >= randThreshold)
            {
                audioSource.PlayOneShot(randomAudio);
                RandomAudioPlayedOnce = true;
            }
            else
                audioSource.PlayOneShot(audBtn.audioClip);
            return true;
        }
        return false;
    }

    private void PlayRandomAudioFromBank(AudioClip[] bank)
    {
        if (!AllAtOnce) { if (!audioSource.isPlaying) { audioSource.PlayOneShot(bank[Random.Range(0, 10)]); } }
        else audioSource.PlayOneShot(bank[Random.Range(0, 10)]);
    }

    private void SetButtons()
    {
        audioSource.Stop();

        #region Sets every button to SiivaGunner's avatar.
        for (int i = 0; i < audioButtons.Length; i++)
            audioButtons[i].UnClick();
        
        foreach(AudioButton btn in audioButtons)
            btn.button.image.material = SiivaAvatar;
        #endregion

        #region Sets the size of the audio buttons when in their 'hidden' states. 
        for (int i = 0; i < audioButtons.Length; i++)
        {
            RectTransform rt = audioButtons[i].button.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(100, 100);
        }
        #endregion
    }

    private void DoTheNicoDance()
    {
        if (PlayAudioWithRandomAudioChance(audioButtons[7], LoudestNigra, 30, 29))
        {
            YazawaNiko.ResetMaterialTimer();
            YazawaNiko.TurnOnAlternateMaterial();
        }
    }

    private void GoOnAdventure()
    {
        if (PlayAudioWithRandomAudioChance(audioButtons[8], LoudestNigra, 30, 29))
        {
            Nozoomy.ResetMaterialTimer();
            Nozoomy.TurnOnAlternateMaterial();
        }
    }

    private void SetImageOnTimer(ref bool swapCheck, Button btn, ref float swapTimer, float timeInterval, int timeLimit, Material original, Material other)
    {
        ///Sets a button's image to an alternate image, for a few seconds.
        if (swapCheck)
        {
            btn.image.material = other;
            swapTimer += timeInterval * Time.deltaTime;
            if (swapTimer >= timeLimit) { swapCheck = false; swapTimer = 0; }
        }
        else btn.image.material = original;
    }

    private void Update()
    {
        #region Sets the correct images for each button at any given time. 
        for (int i = 0; i < 7; i++)
        {
            if (audioButtons[i].hasBeenClicked)
            {
                audioButtons[i].button.image.material = audioButtons[i].defaultImage;
                RectTransform rt = audioButtons[i].button.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(audioButtons[i].defaultWidth, audioButtons[i].defaultHeight);
            }
        }

        if (audioButtons[7].hasBeenClicked)
            YazawaNiko.SetImageOnTimer(audioButtons[7].button);
        
        if (audioButtons[8].hasBeenClicked)
            Nozoomy.SetImageOnTimer(audioButtons[8].button);

        if (audioButtons[9].hasBeenClicked)
        {
            audioButtons[9].button.image.material = audioButtons[9].defaultImage;
            RectTransform rt = audioButtons[9].button.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(audioButtons[9].defaultWidth, audioButtons[9].defaultHeight);
        }
        #endregion

        #region Sets the text colour of the 'All At Once' option under 'Settings'.
        if (AllAtOnce)
            AAOB_Text.color = new Color(0, 0.5F, 0, 1);
        else
            AAOB_Text.color = new Color(0.5F, 0, 0, 1);
        #endregion

        #region Enables and disables three buttons on screen. 
        AllAtOnceButton.gameObject.SetActive(SettingsActive);
        EnableScreamButtonButton.gameObject.SetActive(RandomAudioPlayedOnce && SettingsActive);
        LoudNigraButton.gameObject.SetActive(isScreamButtonEnabled);
        #endregion
    }
}