using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sahnegecisi : MonoBehaviour
{
    public GameObject canvas;

    void Update()
    {
        if(canvas.activeInHierarchy == false)
        {
            SceneManager.LoadScene(1);
        }
    }
}
