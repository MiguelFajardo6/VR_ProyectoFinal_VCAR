using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class BestTimesTable : MonoBehaviour
{
    [SerializeField] private TMP_Text tiemposText;

    [SerializeField] private string fileName = "timeElapsed.json";

    private void Start()
    {
        MostrarTiemposGuardados();
    }

    private void MostrarTiemposGuardados()
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            TimeElapsedList timeElapsedList = JsonUtility.FromJson<TimeElapsedList>(jsonData);

            if (timeElapsedList != null && timeElapsedList.tiempos.Count > 0)
            {
                string tiemposString = "Mejores Tiempos:\n";
                for (int i = 0; i < timeElapsedList.tiempos.Count; i++)
                {
                    tiemposString += (i + 1) + ". " + FormatearTiempo(timeElapsedList.tiempos[i]) + "\n";
                }

                tiemposText.text = tiemposString;
            }
            else
            {
                tiemposText.text = "No hay tiempos guardados.";
            }
        }
        else
        {
            tiemposText.text = "No hay tiempos guardados.";
        }
    }

    private string FormatearTiempo(float tiempo)
    {
        int minutes = (int)(tiempo / 60f);
        int seconds = (int)(tiempo - minutes * 60f);
        int cents = (int)((tiempo - (int)tiempo) * 100f);
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, cents);
    }

    // Clase para almacenar la lista de tiempos para serialización JSON
    [System.Serializable]
    private class TimeElapsedList
    {
        public List<float> tiempos = new List<float>();
    }
}

