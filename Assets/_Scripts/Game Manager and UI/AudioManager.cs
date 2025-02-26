using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioMixer audioMixer;
    public AudioMixerGroup audioMixerMaster;
    public Dictionary<string, AudioSource> audioMap;
    public List<AudioSO> audioSOList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        audioMap = new();
    }

    void Start()
    {
        foreach (AudioSO audioSO in audioSOList)
        {
            AudioSource instance = gameObject.AddComponent<AudioSource>();
            instance.clip = audioSO.clip;
            instance.outputAudioMixerGroup = audioMixerMaster;
            instance.volume = audioSO.volume;
            instance.pitch = audioSO.pitch;
            instance.loop = audioSO.isLoop;

            audioMap[audioSO.name] = instance;
        }

        if (GameManager.Instance.playerData.soundMuted)
        {
            audioMixer.SetFloat("Master", -80f);
        }
        else
        {
            audioMixer.SetFloat("Master", 1f);
        }

        PlayAudioClip("BGM");
    }

    public void PlayAudioClip(string audioName)
    {
        audioMap[audioName].Play();
    }
}
