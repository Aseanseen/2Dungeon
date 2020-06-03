using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
	public TMPro.TMP_Dropdown qualityDropdown;
	public AudioMixer audioMixer;

    public Slider slider;
    static float sliderVal = 0;
    static int qualityIndex = 2;

	// Default to the "High" option
	public void Start(){
		qualityDropdown.value = qualityIndex;
        slider.value = sliderVal;
	}
    public void SetVolume(float volume){
    	audioMixer.SetFloat("MasterVolume", volume);
        saveSlider();
//    	audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }
    public void SetQuality (int qualityIndex){
    	qualityDropdown.value = qualityIndex;
    	saveQuality();
    	QualitySettings.SetQualityLevel(qualityIndex);
    }

    // public void Update()
    // {
    //     Debug.Log(QualitySettings.GetQualityLevel());
    // }

    void saveSlider(){
        sliderVal = slider.value;
    }
    void saveQuality(){
        qualityIndex = qualityDropdown.value;
    }
}
