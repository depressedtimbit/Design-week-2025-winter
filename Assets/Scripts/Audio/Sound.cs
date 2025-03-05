using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "defaultSound", menuName = "Wrappers/Sound")]
///
/// This class wraps a raw sound file - if you create it while having a raw wav file selected, it automatically assigns the name and clip based on that file
/// This class contains a flat volume modifier (makes things a little more convenient)
/// It also contains the subtitle translation of the sound that will come up if you have subtitles enabled
///
public class Sound : ScriptableObject
{
    public AudioClip clip;
    public string subtitleDescription;
    public float volume = 1;
}
