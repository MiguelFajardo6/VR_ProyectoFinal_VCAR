using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPersistente : MonoBehaviour
{
    private static AudioPersistente instancia;
    private AudioSource audioSource;
    public bool musicaActivada = true;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            // Reproducir la música si está activada
            if (musicaActivada && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Comprueba si la música debe estar activa y ajusta la reproducción según corresponda
        if (audioSource.isPlaying && !musicaActivada)
        {
            audioSource.Pause();
        }
        else if (!audioSource.isPlaying && musicaActivada)
        {
            audioSource.Play();
        }
    }
}
