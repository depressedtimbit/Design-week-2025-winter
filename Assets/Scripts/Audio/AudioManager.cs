using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.Audio;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

/// <summary>
/// This script and the referenced scripts within it are derived from a set of audio management scripts that I use in most of my projects.
/// This script loads all objects derived from SoundBase in the Resources/Audio/ProcessedAudio folder, then, using a singleton, can play 
/// any of those sounds from any other script. 
/// 
/// There's some old stuff in it that basically prevents the same sound from being played multiple times in the same frame (to avoid blowing people's
/// eardrums out) - it's not really relevant for us. All that's relevant:
///     - you can use AudioManager.instance.PlaySound(sound name, volume ratio, base pitch, pitch variation) to play a sound from anywhere
///     - you can also use FadeMusicIn / FadeMusicOut with the BGM or AMB tracks to fade the looping background tracks in or out
/// </summary>
public class AudioManager : MonoBehaviour
{
    // the name of the folder to retrieve audio files from
    public string processedAudioFileFolderName = "Audio/ProcessedAudio";

    // basically,there's one folder for raw audio and one folder for Sound instances that have already been created
    // since it's cumbersome to make a new Sound instance for every single sound, the ones that need to have certain stuff manually edited (e.g. volume)
    // will have their own Sound instances made and placed in ProcessedAudio, while the rest of the files are taken straight from RawAudio and automatically processed into
    // Sound instances

    private GameObject audioSourcePrefab;
    private AudioMixer masterMixer;

    public static AudioManager instance;

    [System.Serializable]
    public class NameSourcePair
    {
        public string sourceName;
        public AudioSource source;
    }

    [System.Serializable]
    public class NameSoundPair
    {
        public string soundName;
        public string sound;
    }

    public static List<NameSoundPair> allSounds;
    private Dictionary<string, Sound> soundDict;

    public class PlaySoundInfo
    {
        public string soundName;
        public float volumeRatio;
        public Vector3 targPos;
        public float spatialBlend = 1;
        public float lifeTime = 1.5f;
        public string mixerGroup = "SFX";
        public float pitch = 1;

        public PlaySoundInfo(string soundName, Vector3 targPos, float volumeRatio = 1, float spatialBlend = 1, float lifeTime = 1.5f, string mixerGroup = "SFX", float pitch = 1)
        {
            this.soundName = soundName;
            this.volumeRatio = volumeRatio;
            this.targPos = targPos;
            this.spatialBlend = spatialBlend;
            this.lifeTime = lifeTime;
            this.mixerGroup = mixerGroup;
            this.pitch = pitch;
        }
    }

    private List<PlaySoundInfo> soundsToPlayInfo;
    private List<string> soundsThisFrame;

    // audio player reserved for bgm tracks
    //public AudioSource musicPlayer;
    public List<NameSourcePair> loopPlayers;

    private Dictionary<string, AudioSource> loopPlayersDict;

    // main sound player, we can use PlayOneShot to play multiple sounds on it
    public AudioSource soundPlayer;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;

            audioSourcePrefab = Resources.Load<GameObject>("Audio/AudioSourceObject");

            masterMixer = Resources.Load<AudioMixer>("Audio/MasterMixer");

            DontDestroyOnLoad(gameObject);
        } else
        {
            print("There are multiple AudioManagers - destroying new one");
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        soundsToPlayInfo = new List<PlaySoundInfo>();
        soundsThisFrame = new List<string>();

        soundDict = new Dictionary<string, Sound>();

        // TODO: retrieve all complete Sounds from the first folder
        // + create Sounds from all raw audio files in the second folder

        loopPlayersDict = new Dictionary<string, AudioSource>();

        foreach (NameSourcePair pair in loopPlayers)
        {
            loopPlayersDict.Add(pair.sourceName, pair.source);
        }

        Sound[] processedSounds = Resources.LoadAll<Sound>(processedAudioFileFolderName);

        foreach (Sound s in processedSounds)
        {
            if (!soundDict.ContainsKey(s.name))
            {
                //print("adding processed sound: " + s.soundName);
                soundDict.Add(s.name, s);
            }
        }

        // all our audio will be processed - enforcing subtitles

        /*
        AudioClip[] rawClips = Resources.LoadAll<AudioClip>(rawAudioFileFolderName);

        Sound sn;
        foreach (AudioClip c in rawClips)
        {
            sn = (Sound)ScriptableObject.CreateInstance("Sound");
            sn.clip = c;
            sn.soundName = c.name;
            if (!soundDict.ContainsKey(sn.soundName))
            {
                //print("adding raw sound: " + sn.soundName);
                soundDict.Add(sn.soundName, sn);
            }
        }
        */

    }


    // Update is called once per frame
    public void SetTrackVolume(string trackName, float volumePercent)
    {
        // the value in the log has to be between 0 and 1
        masterMixer.SetFloat(trackName, Mathf.Log10(Mathf.Clamp(volumePercent / 100, 0.0001f, 1f)) * 20);
    }

    public void SetPlayerVolume(string trackName, float volumePercent)
    {
        // the value in the log has to be between 0 and 1
        AudioSource player = loopPlayersDict[trackName];
        player.volume = volumePercent;
    }



    public void RequestSound(PlaySoundInfo inf)
    {
        if (inf == null) return;
        if (!soundsThisFrame.Contains(inf.soundName))
        {
            soundsThisFrame.Add(inf.soundName);
            soundsToPlayInfo.Add(inf);
        }
    }




    public LTDescr FadeMusicOut(string source = "BGM", float fadeTime = 1f, string fadeInNew = "", float fadeNewVolume = 1f)
    {
        AudioSource musicPlayer = loopPlayersDict[source];
        LeanTween.cancel(musicPlayer.gameObject);
        return LeanTween.value(musicPlayer.volume, 0, fadeTime).setOnUpdate((float val) =>
        {
            musicPlayer.volume = val;

            if (fadeInNew != "")
            {
                PlayMusic(fadeInNew, 0, source);
                FadeMusicIn(source, fadeNewVolume, fadeTime);
            }
        });

    }

    public void FadeMusicIn(string source, float volume, float fadeTime = 1f)
    {
        AudioSource musicPlayer = loopPlayersDict[source];
        LeanTween.cancel(musicPlayer.gameObject);
        LeanTween.value(musicPlayer.gameObject, musicPlayer.volume, volume, fadeTime).setOnUpdate((float val) =>
        {
            musicPlayer.volume = val;
        });
    }

    public void StopMusic(string source = "BGM")
    {
        loopPlayersDict[source].Stop();
    }

    public void StopAllMusic()
    {
        foreach (string key in loopPlayersDict.Keys)
        {
            loopPlayersDict[key].Stop();
        }
    }

    public void PlayMusic(string trackName, float volRatio, string source = "BGM")
    {
        AudioSource musicPlayer = loopPlayersDict[source];

        if (!soundDict.ContainsKey(trackName))
        {
            Debug.LogError("Track not found!");
            return;
        }

        Sound sound = soundDict[trackName];
        musicPlayer.Stop();
        musicPlayer.clip = sound.clip;
        musicPlayer.volume = sound.volume * volRatio;
        musicPlayer.loop = true;
        musicPlayer.Play();
    }



    public void PlaySound(string sound, float volume = 1f, float volumeVariation = 0f, float pitchVariation = 0, float basePitch = 1f)
    {
        if (!soundDict.ContainsKey(sound))
        {
            Debug.LogError($"AudioManager: Sound '{sound}' not found.");
            return;
        }

        Sound soundToPlay = soundDict[sound];

        soundPlayer.volume = volume + Random.Range(-volumeVariation, volumeVariation);
        float p = basePitch + Random.Range(-pitchVariation, pitchVariation);
        soundPlayer.pitch = p;
        soundPlayer.PlayOneShot(soundToPlay.clip);

        if (SubtitleManager.instance != null)
        {
            SubtitleManager.instance.DoSubtitle(soundToPlay.subtitleDescription);
        }

    }



}
