using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    // Game volume
    public float volume = 100f;
    public float musicVolume = 100f;
    public float soundVolume = 100f;

    // Main AudioSource
    private AudioSource _levelAudio;
    private AudioClip[] _musicList;
    private AudioClip[] _soundFXList;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }
    // Use this for initialization
    void Start () {
        _levelAudio = transform.GetComponent<AudioSource>();

        /// <summary>
        /// List of Game Music
        /// 0:  Main menu music
        /// 1:  Area 1 music
        /// 2:  Area 2 music
        /// 3:  Town music
        /// </summary>
        _musicList = new AudioClip[] {(AudioClip)Resources.Load("Sound/Music/mainmenu"),
                                        (AudioClip)Resources.Load("Sound/Music/area1"),
                                        (AudioClip)Resources.Load("Sound/Music/area2"),
                                        (AudioClip)Resources.Load("Sound/Music/town")};

        /// <summary>
        /// List of Non-Skill Sound FX
        /// 0:  
        /// </summary>
        _soundFXList = new AudioClip[] {};

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Plays sound on AudioSource
    public void PlaySound(AudioSource source) {
        source.PlayOneShot(source.clip, (volume / 100) * (soundVolume / 100));
    }

    // Plays a clip at AudioSource
    public void PlaySound(AudioSource source, AudioClip sound) {
        source.PlayOneShot(sound, (volume / 100) * (soundVolume / 100));
    }

    // Plays a clip at AudioSource with delay
    public void PlaySound(AudioSource source, AudioClip sound, float delay) {
        source.clip = sound;
        source.volume = (volume / 100) * (soundVolume / 100);
        source.PlayDelayed(delay);
    }

    // Plays a clip fxID at AudioSource
    public void PlaySound(AudioSource source, int soundID) {
        source.PlayOneShot(_soundFXList[soundID], (volume / 100)*(soundVolume/100));
    }

    // Plays musicID on main AudioSource
    public void PlayMusic(int musicID) {
        _levelAudio.Stop();
        _levelAudio.volume = (volume / 100) * (musicVolume / 100);
        _levelAudio.loop = true;
        _levelAudio.clip = _musicList[musicID];
        _levelAudio.Play();
    }


    // Stops main AudioSource
    public void StopMusic() {
        _levelAudio.Stop();
    }
}
