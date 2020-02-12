using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{
    private AudioSource source;
    public AudioClip[] sounds;

   
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = GetRandom();
    }

    public AudioClip GetRandom()
    {
        int randSound = Random.Range(0, sounds.Length);
        return sounds[randSound];
    }

    public void Reproducir()
    {
        source.Play();
    }
}
