using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundCount : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text bestTimeText; // Nuevo campo para mostrar el mejor tiempo
    public Check check1;
    public Check check2;
    public Text textoVueltas;
    public GameManager gameManager;

    [SerializeField] private string fileName = "timeElapsed.json";

    private bool countingTime = false;
    private bool initialTime = true;
    private float timeElapsed;
    private float bestTime = float.MaxValue; // Inicializar el mejor tiempo con un valor máximo
    private int minutes, seconds, cents;
    private int vueltas = 0;

    private void Start()
    {
        // Mostrar el mejor tiempo al iniciar el juego
        MostrarMejorTiempo();
    }

    public void Update()
    {
        if (check1.GetCollisionExit() && initialTime)
        {
            countingTime = true;
            timeElapsed = 0f;
            initialTime = false;
            check1.ResetCollisionOcurred();
        } 
        

        if (countingTime)
        {
            timeElapsed += Time.deltaTime;
            
            timerText.text = FormatearTiempo(timeElapsed);

            
        }

        if (check2.GetCollisionExit() && check1.GetCollisionExit() && check1.GetCollisionState())
        {
            Debug.Log("Ambos checks pasados");
            vueltas++;
            textoVueltas.text = "Vueltas: " + vueltas.ToString();

            SaveTimeElapsed();
            // Actualizar el mejor tiempo si se supera
            if (timeElapsed < bestTime)
            {
                bestTime = timeElapsed;
                bestTimeText.text = FormatearTiempo(bestTime);
            }
            check2.ResetCollisionState();
            check1.ResetCollisionState();
            countingTime = false;
            initialTime = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        countingTime = false;
        initialTime = true;
        check1.ResetCollisionState();
        check2.ResetCollisionState();
        gameManager.Spawn();
    }

    public void Restart()
    {
        countingTime = false;
        initialTime = true;
        check1.ResetCollisionState();
        check2.ResetCollisionState();
        gameManager.Spawn();
    }

    private void SaveTimeElapsed()
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // Crear o cargar la lista de tiempos
        List<float> tiempos = new List<float>();
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            tiempos = JsonUtility.FromJson<TimeElapsedList>(jsonData).tiempos;
        }

        // Agregar el nuevo tiempo a la lista
        tiempos.Add(timeElapsed);

        // Ordenar la lista de tiempos de menor a mayor
        tiempos.Sort();

        // Mantener solo los 5 mejores tiempos
        if (tiempos.Count > 5)
        {
            tiempos.RemoveRange(5, tiempos.Count - 5);
        }

        // Convertir la lista ordenada a JSON
        string updatedJsonData = JsonUtility.ToJson(new TimeElapsedList { tiempos = tiempos });

        // Guardar JSON en el archivo
        File.WriteAllText(filePath, updatedJsonData);
    }

    private void MostrarMejorTiempo()
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            TimeElapsedList timeElapsedList = JsonUtility.FromJson<TimeElapsedList>(jsonData);

            if (timeElapsedList != null && timeElapsedList.tiempos.Count > 0)
            {
                bestTime = timeElapsedList.tiempos[0]; // El mejor tiempo es el primero en la lista
                bestTimeText.text = FormatearTiempo(bestTime);
            }
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
