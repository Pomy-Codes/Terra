using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private enum VolumeType { 
        Master,
        Music, 
        SFX, 
        Ambience 
    }
    [Header("Type")]
    [SerializeField] private VolumeType volumeType;
    private Slider volumeSlider;
    private void Awake()
    {
        volumeSlider = this.GetComponentInChildren<Slider>();
    }
    private void Update()
    {
        switch (volumeType)
        {
            case VolumeType.Master:
                volumeSlider.value = AudioManager.instance.MasterVolume;
                break;
            case VolumeType.Music:
                volumeSlider.value = AudioManager.instance.MusicVolume;
                break;
            case VolumeType.SFX:
                volumeSlider.value = AudioManager.instance.SFXVolume;
                break;
            case VolumeType.Ambience:
                volumeSlider.value = AudioManager.instance.AmbienceVolume;
                break;
            default:
                Debug.LogWarning("VolumeType not set! " + volumeType);
                break;
        }
    }
    public void OnSliderValueChanged() {
        switch (volumeType)
        {
            case VolumeType.Master:
                AudioManager.instance.MasterVolume = volumeSlider.value;
                break;
            case VolumeType.Music:
                AudioManager.instance.MusicVolume = volumeSlider.value;
                break;
            case VolumeType.SFX:
                AudioManager.instance.SFXVolume = volumeSlider.value;
                break;
            case VolumeType.Ambience:
                AudioManager.instance.AmbienceVolume = volumeSlider.value;
                break;
            default:
                Debug.LogWarning("VolumeType not set! " + volumeType);
                break;
        }

    }
        
}
