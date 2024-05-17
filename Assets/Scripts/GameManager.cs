using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int index;
    public GameObject[] cars;
    public Text speedometerText; // Referencia al objeto de texto en tu escena

    

    private GameObject currentCar; // Mantener una referencia al coche actual

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    public void Spawn()
    {
        // Destruir el coche anterior si existe
        if (currentCar != null)
        {
            Destroy(currentCar);
        }

        index = PlayerPrefs.GetInt("carIndex");
        currentCar = Instantiate(cars[index], Vector3.zero, Quaternion.identity);

        // Obtener el componente CarController del coche instanciado
        CarController carController = currentCar.GetComponent<CarController>();

        if (carController != null)
        {
            // Asignar el objeto de texto existente al velocímetro del coche instanciado
            carController.speedometerText = speedometerText;
            
        }
        else
        {
            Debug.LogError("No se encontró un componente CarController en el prefab del coche.");
        }
    }

}
