using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AudioButton
{
    [SerializeField]
    private Button _button;

    public Button button { get { return _button; } private set { _button = value; } }

    [SerializeField]
    private AudioClip _audioClip;

    public AudioClip audioClip { get { return _audioClip; } private set { _audioClip = value; } }

    [SerializeField]
    private float _defaultWidth;

    public float defaultWidth { get { return _defaultWidth; } private set { _defaultWidth = value; } }

    [SerializeField]
    private float _defaultHeight;

    public float defaultHeight { get { return _defaultHeight; } private set { _defaultHeight = value; } }

    [SerializeField]
    private bool _hasBeenClicked;

    public bool hasBeenClicked { get { return _hasBeenClicked; } private set { _hasBeenClicked = value; } }

    /*[SerializeField]
    private Material _defaultImage;

    public Material defaultImage { get { return _defaultImage; } private set { _defaultImage = value; } }*/

    public Material defaultImage { get; private set; }

    public void Click ()
    {
        hasBeenClicked = true;
    }

    public void UnClick ()
    {
        hasBeenClicked = false;
    }

    public void SetWidthAndHeight ()
    {
        defaultWidth = button.GetComponent<RectTransform>().sizeDelta.x;
        defaultHeight = button.GetComponent<RectTransform>().sizeDelta.y;
    }

    public void SetDefaultImage ()
    {
        defaultImage = button.GetComponent<Image>().material;
    }
}

public class SiIvaGunner_Soundboard : MonoBehaviour
{
    public AudioSource _audioSource;
    public AudioClip LoudestNigra;
    public Text AAOB_Text;

    private bool enableScreamButton;
    private bool RandomAudioPlayedOnce, SettingsActive;    
    private bool AllAtOnce = false;    

    [Header("Grand Dad Reaction Bank")]
    public AudioClip[] GrandDadReactionBank;

    [Header("Audio Buttons")]
    public AudioButton[] _AudioButtons;

    [Header("Other Buttons")]
    public Button resetButton;
    public Button SettingsButton;
    public Button AllAtOnceButton;
    public Button EnableScreamButtonButton;
    public Button LoudNigraButton;

    [Header("Nozoomy Objects")]
    public Material HappyNozoomy;
    public Material SadNozoomy;
    public bool beHappy;
    public float adventureTimer;

    [Header("Nico-Nii Objects")]
    public Material AnnoyedNico;
    public Material NicoNico;
    public bool beDancing;
    public float danceTimer;

    [Header("The one and only")]
    public Material SiivaAvatar;
    
    /// <summary>
    /// For use with button listeners. Paste this in CodeCrap and I will end you.
    /// </summary>
    private void SetBool(ref bool theBool) { theBool = !theBool; } 

    private void Start()
    {
        RandomAudioPlayedOnce = enableScreamButton = false;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        for (int i = 0; i < _AudioButtons.Length; i++)
        {
            _AudioButtons[i].SetDefaultImage();
            _AudioButtons[i].SetWidthAndHeight();
        }

        SetButtons();

        //A click is defined as a push AND a release of the button. The method below plays on release.
                
        _AudioButtons[0].button.onClick.AddListener(delegate
        { PlayAudioWithRandomAudioChance(_AudioButtons[0], LoudestNigra, 30, 29); });
        _AudioButtons[1].button.onClick.AddListener(delegate
        { PlayAudioWithRandomAudioChance(_AudioButtons[1], LoudestNigra, 30, 29); });
        _AudioButtons[2].button.onClick.AddListener(delegate
        { PlayAudioWithRandomAudioChance(_AudioButtons[2], LoudestNigra, 30, 29); });
        _AudioButtons[3].button.onClick.AddListener(delegate
        { PlayAudioWithRandomAudioChance(_AudioButtons[3], LoudestNigra, 30, 29); });
        _AudioButtons[4].button.onClick.AddListener(delegate
        { PlayAudioWithRandomAudioChance(_AudioButtons[4], LoudestNigra, 30, 29); });
        _AudioButtons[5].button.onClick.AddListener(delegate
        { PlayAudioWithRandomAudioChance(_AudioButtons[5], LoudestNigra, 30, 29); });
        _AudioButtons[6].button.onClick.AddListener(delegate
        { PlayAudioWithRandomAudioChance(_AudioButtons[6], LoudestNigra, 30, 29); });
        _AudioButtons[7].button.onClick.AddListener(DoTheNicoDance);
        _AudioButtons[8].button.onClick.AddListener(GoOnAdventure);
        _AudioButtons[9].button.onClick.AddListener(delegate
        { PlayAudio(_AudioButtons[9]); });

        EnableScreamButtonButton.onClick.AddListener(delegate { SetBool(ref enableScreamButton); });
        resetButton.onClick.AddListener(SetButtons);
        AllAtOnceButton.onClick.AddListener(delegate { SetBool(ref AllAtOnce); });
        SettingsButton.onClick.AddListener(delegate { SetBool(ref SettingsActive); });
    }

    private bool PlayAudio(AudioButton audBtn)
    {
        if ((!AllAtOnce && !_audioSource.isPlaying) || AllAtOnce)
        {
            audBtn.Click();
            _audioSource.PlayOneShot(audBtn.audioClip);
            return true;
        }
        return false;
    }

    private bool PlayAudioWithRandomAudioChance(AudioButton audBtn, AudioClip randomAudio, int randNum, int randThreshold)
    {
        if ((!AllAtOnce && !_audioSource.isPlaying) || AllAtOnce)
        {
            audBtn.Click();
            if (Random.Range(0, randNum) >= randThreshold)
            {
                _audioSource.PlayOneShot(randomAudio);
                RandomAudioPlayedOnce = true;
            }
            else
                _audioSource.PlayOneShot(audBtn.audioClip);
            return true;
        }
        return false;
    }

    private void PlayRandomAudioFromBank(AudioClip[] bank)
    {
        if (!AllAtOnce) { if (!_audioSource.isPlaying) { _audioSource.PlayOneShot(bank[Random.Range(0, 10)]); } }
        else _audioSource.PlayOneShot(bank[Random.Range(0, 10)]);
    }

    private void SetButtons()
    {
        _audioSource.Stop();

        #region Sets every button to SiivaGunner's avatar.
        for (int i = 0; i < _AudioButtons.Length; i++)
            _AudioButtons[i].UnClick();
        
        foreach(AudioButton btn in _AudioButtons)
            btn.button.image.material = SiivaAvatar;
        #endregion

        #region Sets the size of the audio buttons when in their 'hidden' states. 
        for (int i = 0; i < _AudioButtons.Length; i++)
        {
            RectTransform rt = _AudioButtons[i].button.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(100, 100);
        }
        #endregion
    }

    private void DoTheNicoDance()
    {
        if (PlayAudioWithRandomAudioChance(_AudioButtons[7], LoudestNigra, 30, 29))
        {
            danceTimer = 0;
            beDancing = true;
        }
    }

    private void GoOnAdventure()
    {
        if (PlayAudioWithRandomAudioChance(_AudioButtons[8], LoudestNigra, 30, 29))
        {
            adventureTimer = 0;
            beHappy = true;
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
            if (_AudioButtons[i].hasBeenClicked)
            {
                _AudioButtons[i].button.image.material = _AudioButtons[i].defaultImage;
                RectTransform rt = _AudioButtons[i].button.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(_AudioButtons[i].defaultWidth, _AudioButtons[i].defaultHeight);
            }
        }

        if (_AudioButtons[7].hasBeenClicked)
            SetImageOnTimer(ref beDancing, _AudioButtons[7].button, ref danceTimer, 1, 5, AnnoyedNico, NicoNico);

        if (_AudioButtons[8].hasBeenClicked)
            SetImageOnTimer(ref beHappy, _AudioButtons[8].button, ref adventureTimer, 1, 5, SadNozoomy, HappyNozoomy);

        if (_AudioButtons[9].hasBeenClicked)
        {
            _AudioButtons[9].button.image.material = _AudioButtons[9].defaultImage;
            RectTransform rt = _AudioButtons[9].button.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(_AudioButtons[9].defaultWidth, _AudioButtons[9].defaultHeight);
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
        LoudNigraButton.gameObject.SetActive(enableScreamButton);
        #endregion
    }
}