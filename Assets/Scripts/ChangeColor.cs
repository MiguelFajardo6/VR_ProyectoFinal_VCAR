using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ChangeColor : MonoBehaviour
{
    public Material[] materiales;
    private Renderer renderer;
    private Material currentMaterial;

    private const string PrefabColorIndexKey = "PrefabColorIndex";

    void Start()
    {
        renderer = GetComponent<Renderer>();
        int savedIndex = PlayerPrefs.GetInt(PrefabColorIndexKey, 0);
        currentMaterial = materiales[savedIndex];
        renderer.material = currentMaterial;
    }

    public void CambiarColor()
    {
        int indiceSiguiente = (System.Array.IndexOf(materiales, currentMaterial) + 1) % materiales.Length;
        currentMaterial = materiales[indiceSiguiente];
        renderer.material = currentMaterial;
        PlayerPrefs.SetInt(PrefabColorIndexKey, indiceSiguiente);
    }
}
