using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    // Game volume
    public float volume = 100f;
    public float musicVolume = 100f;
    public float soundVolume = 100f;

    // Main AudioSource
    private AudioSource _levelAudio;
    private AudioSource _audioUI;
    private AudioClip[] _musicList;
    private AudioClip[] soundFXList;

    private void Awake() {
        
        if (instance == null) {
            instance = this;
        }
        else {
            Debug.Log("Found two SoundManager Instances.. Destorying new one");
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);

        _levelAudio = GetComponent<AudioSource>();
        _audioUI = GetComponentInChildren<AudioSource>();

        /// <summary>
        /// List of Game Music
        /// 0:  Main menu music
        /// 1:  Town music
        /// 2:  Safe Zone music
        /// 3:  Area 1 music
        /// 4:  Area 2 music
        /// 5:  Area 3 music
        /// </summary>
        _musicList = new AudioClip[] {(AudioClip)Resources.Load("Sound/Music/mainmenu"),
                                        (AudioClip)Resources.Load("Sound/Music/town"),
                                        (AudioClip)Resources.Load("Sound/Music/safe"),
                                        (AudioClip)Resources.Load("Sound/Music/area1"),
                                        (AudioClip)Resources.Load("Sound/Music/area2"),
                                        (AudioClip)Resources.Load("Sound/Music/area3")};
    
        /// <summary>
        /// List of Non-Skill Sound FX
        /// 0:  
        /// </summary>
        soundFXList = new AudioClip[] { (AudioClip)Resources.Load("Sound/FX/buttonclick") };
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Button[] buttons = Resources.FindObjectsOfTypeAll<Button>();
        foreach (Button b in buttons) {
            b.onClick.AddListener(delegate { PlayUISound(0); });
        }
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
        source.PlayOneShot(soundFXList[soundID], (volume / 100)*(soundVolume/100));
    }

    // Plays a clip fxID at UI AudioSource
    public void PlayUISound(int soundID) {
        _audioUI.PlayOneShot(soundFXList[soundID], (volume / 100) * (soundVolume / 100));
    }

    // Plays musicID on main AudioSource
    public void PlayMusic(int musicID) {
        if(_levelAudio.clip != _musicList[musicID] || !_levelAudio.isPlaying) {
            _levelAudio.Stop();
            _levelAudio.volume = (volume / 100) * (musicVolume / 100);
            _levelAudio.loop = true;
            _levelAudio.clip = _musicList[musicID];
            _levelAudio.Play();
        }
    }


    // Stops main AudioSource
    public void StopMusic() {
        _levelAudio.Stop();
    }
}
