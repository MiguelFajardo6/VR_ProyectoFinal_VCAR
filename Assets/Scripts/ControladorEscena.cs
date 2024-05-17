using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorEscena : MonoBehaviour
{
    private AudioPersistente instanciaAudio;

    private void Start()
    {
        // Obtén la instancia de AudioPersistente
        instanciaAudio = FindObjectOfType<AudioPersistente>();
        // Desactiva la música si la instancia existe y la música está activada
        if (instanciaAudio != null && instanciaAudio.musicaActivada)
        {
            instanciaAudio.musicaActivada = false;
        }
    }

    private void OnDestroy()
    {
        // Activa la música si la instancia existe y la música estaba activada originalmente
        if (instanciaAudio != null && !instanciaAudio.musicaActivada)
        {
            instanciaAudio.musicaActivada = true;
        }
    }
}
