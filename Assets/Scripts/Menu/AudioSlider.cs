using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    public void UpdateVolume()
    {
        if(mixer!= null)
        {
            mixer.SetFloat("Master", Mathf.Log10(slider.value) * 20);
        }
        
    }
}
