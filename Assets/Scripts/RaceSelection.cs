using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceSelection : MonoBehaviour
{
    public GameObject[] races;
    public GameObject[] points;
    public Button next;
    public Button prev;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;

        for(int i = 0; i < races.Length; i++)
        {
            races[i].SetActive(false);
            races[index].SetActive(true);
            points[i].SetActive(false);
            points[index].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(index >= 1)
        {
            next.interactable = false;
        }
        else
        {
            next.interactable = true;
        }

        if(index <= 0)
        {
            prev.interactable = false;
        }
        else
        {
            prev.interactable = true;
        }
    }
    public void Next()
    {
        index++;
        for(int i = 0; i< races.Length; i++)
        {
            races[i].SetActive(false);
            races[index].SetActive(true);
            points[i].SetActive(false);
            points[index].SetActive(true);
        }
        
    }
    public void Prev()
    {
        index--;
        for (int i = 0; i < races.Length; i++)
        {
            races[i].SetActive(false);
            races[index].SetActive(true);
            points[i].SetActive(false);
            points[index].SetActive(true);
        }
        
    }

    public void Race()
    {
        if(index == 0)
        {
            SceneManager.LoadSceneAsync("Game");
        }
        else if(index == 1)
        {
            SceneManager.LoadSceneAsync("Game2");
        }

    }
}
