using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorEscena : MonoBehaviour
{
    private AudioPersistente instanciaAudio;

    private void Start()
    {
        // Obt�n la instancia de AudioPersistente
        instanciaAudio = FindObjectOfType<AudioPersistente>();
        // Desactiva la m�sica si la instancia existe y la m�sica est� activada
        if (instanciaAudio != null && instanciaAudio.musicaActivada)
        {
            instanciaAudio.musicaActivada = false;
        }
    }

    private void OnDestroy()
    {
        // Activa la m�sica si la instancia existe y la m�sica estaba activada originalmente
        if (instanciaAudio != null && !instanciaAudio.musicaActivada)
        {
            instanciaAudio.musicaActivada = true;
        }
    }
}
