using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; // Required when Using UI elements.
using TMPro;
using UnityEngine.Audio;
public class Options : MonoBehaviour
{
    public Slider mainSlider;
    public TextMeshProUGUI percent;
    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public void Start()
    {
        initResolutions();

        mainSlider.wholeNumbers = true;
        //Adds a listener to the main slider and invokes a method when the value changes.
        mainSlider.onValueChanged.AddListener(delegate { SliderValueChangeCheck(); });
        float vol;
        audioMixer.GetFloat("volume", out vol);
        mainSlider.value = vol;
        //Add listener for when the value of the Dropdown changes, to take action
        resolutionDropdown.onValueChanged.AddListener(delegate{setResolution(resolutionDropdown);});
    }

    // Invoked when the value of the slider changes.
    public void SliderValueChangeCheck()
    {
        Debug.Log(mainSlider.value);
        double percentText = (mainSlider.value + 80) * 1.25;
        percent.text = (int)percentText + "%";
        audioMixer.SetFloat("volume", mainSlider.value);
    }

    public void SetFullscreen()
    {
        Screen.fullScreen = true;
    }

    public void initResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
               resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

    }
    public void setResolution(TMPro.TMP_Dropdown change)
    {
        Resolution resolution = resolutions[change.value];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }
}
