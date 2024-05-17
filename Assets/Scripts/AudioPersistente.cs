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
            // Reproducir la m�sica si est� activada
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
        // Comprueba si la m�sica debe estar activa y ajusta la reproducci�n seg�n corresponda
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
