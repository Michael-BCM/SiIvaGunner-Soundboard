using UnityEngine;
using UnityEngine.UI;

namespace SiIvaGunner_Soundboard_Namespace
{
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
        private bool _hasBeenClicked;

        public bool hasBeenClicked { get { return _hasBeenClicked; } private set { _hasBeenClicked = value; } }

        public Material defaultImage { get; private set; }
        public float defaultWidth { get; private set; }
        public float defaultHeight { get; private set; }

        public void Click() { hasBeenClicked = true; }
        public void UnClick() { hasBeenClicked = false; }

        public void SetWidthAndHeight()
        {
            defaultWidth = button.GetComponent<RectTransform>().sizeDelta.x;
            defaultHeight = button.GetComponent<RectTransform>().sizeDelta.y;
        }

        public void SetDefaultImage()
        {
            defaultImage = button.GetComponent<Image>().material;
        }
    }

    [System.Serializable]
    public class SwapMaterialOnPress
    {
        [SerializeField]
        private Material defaultMaterial;

        [SerializeField]
        private Material alternateMaterial;

        [SerializeField]
        private bool alternateMaterialOn;

        [SerializeField]
        private float alternateMaterialTimer;

        [SerializeField]
        private float timeInterval;

        [SerializeField]
        private float timeLimit;

        public void ResetMaterialTimer()
        {
            alternateMaterialTimer = 0;
        }

        public void TurnOnAlternateMaterial()
        {
            alternateMaterialOn = true;
        }

        public void TurnOffAlternateMaterial()
        {
            alternateMaterialOn = false;
        }

        public void SetImageOnTimer(Button button)
        {
            if (alternateMaterialOn)
            {
                button.image.material = alternateMaterial;
                alternateMaterialTimer += timeInterval * Time.deltaTime;
                if (alternateMaterialTimer >= timeLimit) { TurnOffAlternateMaterial(); }
            }
            else button.image.material = defaultMaterial;
        }
    }
}