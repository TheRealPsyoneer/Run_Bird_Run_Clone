using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
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
            audioMap[audioSO.name] = instance;
        }
    }

    public void PlayAudioClip(string audioName)
    {
        audioMap[audioName].Play();
    }
}
