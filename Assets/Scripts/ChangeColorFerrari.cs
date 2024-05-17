using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorFerrari : MonoBehaviour
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
        // Cambiar el material del sub-mesh en el índice 1
        Material[] currentMaterials = renderer.materials;
        currentMaterials[1] = currentMaterial;
        renderer.materials = currentMaterials;
    }

    public void CambiarColor()
    {
        int indiceSiguiente = (System.Array.IndexOf(materiales, currentMaterial) + 1) % materiales.Length;
        currentMaterial = materiales[indiceSiguiente];
        // Cambiar el material del sub-mesh en el índice 1
        Material[] currentMaterials = renderer.materials;
        currentMaterials[1] = currentMaterial;
        renderer.materials = currentMaterials;
        PlayerPrefs.SetInt(PrefabColorIndexKey, indiceSiguiente);
    }
}
