using Mvc.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mvc.Controllers
{
    public class SettingController : MonoBehaviour
    {
        [SerializeField] private Sprite spriteAudioOff;
        [SerializeField] private Sprite spriteAudioOn;
        [SerializeField] private Image imageAudioPion;
        [SerializeField] private Slider sliderVolumeAudioPion;
        [SerializeField] private Image imageAudioAutres;
        [SerializeField] private Slider sliderVolumeAudioAutres;
        [SerializeField] private Image imageAudioBouton;
        [SerializeField] private Slider sliderVolumeAudioBouton;

        private void Awake()
        {
            initialiseVolume();
        }

        public void changerDeScene(string nomScene)
        {
            Fonctions.changerDeScene(nomScene);
        }
        public void volumeChange(string typeAudio)
        {
            if (typeAudio.CompareTo("AudioPion") == 0)
            {
                PlayerPrefs.SetFloat("volumeAudioPion", sliderVolumeAudioPion.value);
                Fonctions.changerSprite(imageAudioPion, sliderVolumeAudioPion.value == 0 ? spriteAudioOff : spriteAudioOn);
            }
            else if (typeAudio.CompareTo("AudioBouton") == 0)
            {
                PlayerPrefs.SetFloat("volumeAudioBouton", sliderVolumeAudioBouton.value);
                Fonctions.changerSprite(imageAudioBouton, sliderVolumeAudioBouton.value == 0 ? spriteAudioOff : spriteAudioOn);
            }
            else if (typeAudio.CompareTo("AudioAutres") == 0)
            {
                PlayerPrefs.SetFloat("volumeAudioAutres", sliderVolumeAudioAutres.value);
                Fonctions.changerSprite(imageAudioAutres, sliderVolumeAudioAutres.value == 0 ? spriteAudioOff : spriteAudioOn);
            }
            Fonctions.changerVolumeAudios();
        }
        public void initialiseVolume()
        {
            sliderVolumeAudioPion.value = PlayerPrefs.GetFloat("volumeAudioPion");
            sliderVolumeAudioAutres.value = PlayerPrefs.GetFloat("volumeAudioAutres");
            sliderVolumeAudioBouton.value = PlayerPrefs.GetFloat("volumeAudioBouton");
            Fonctions.changerSprite(imageAudioPion, sliderVolumeAudioPion.value == 0 ? spriteAudioOff : spriteAudioOn);
            Fonctions.changerSprite(imageAudioBouton, sliderVolumeAudioBouton.value == 0 ? spriteAudioOff : spriteAudioOn);
            Fonctions.changerSprite(imageAudioAutres, sliderVolumeAudioAutres.value == 0 ? spriteAudioOff : spriteAudioOn);
        }
    }
}
