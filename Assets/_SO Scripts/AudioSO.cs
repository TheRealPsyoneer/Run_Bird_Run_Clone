using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio", menuName = "Audio SO")]
public class AudioSO : ScriptableObject
{
    public AudioClip clip;
    public AudioSource audioSource;

    [Range(0f, 1f)] 
    public float volume;
    [Range(0.1f,3f)] 
    public float pitch;
}
