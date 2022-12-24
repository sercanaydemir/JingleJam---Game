using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class wave
{
    public string WaveName;
    public int numberofenemies;
    public GameObject[] typeofenemies;
    public float spawnInterval;

}

public class wavespawner : MonoBehaviour
{
    public wave[] waves;
    private wave currentwave;
    Vector3 spawnarea;
    int currentwavenumber = 0;
    private float enemyspawntime;
    public float[] spawnareaX;
    public float[] spawnareaZ;
    public float spawny = 0;
    int index1;
    int index2;
    private void Start()
    {
        currentwave = waves[currentwavenumber];
        enemyspawntime = currentwave.spawnInterval;
    }

    void Update()
    {
        currentwave = waves[currentwavenumber];
        spawnvawe();
    }

    private void spawnvawe()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        if (currentwave.numberofenemies == 0)
        {
            if (enemies.Length == 0)
            {
               currentwavenumber += 1;
            }          
        }
        if (enemyspawntime <= 0)
        {
            enemyspawntime = currentwave.spawnInterval;
            index1 = -2;
            index2 = -1;
            for (int j = 0; j < 4; j++)
            {
                index1 += 2;
                index2 += 2;
                spawnarea = new Vector3(Random.Range(spawnareaX[index1], spawnareaX[index2]), spawny, Random.Range(spawnareaZ[index1], spawnareaZ[index2]));
    
                int i = Random.Range(0, 10);
                if (i <= 10)
                {    
                    Instantiate(currentwave.typeofenemies[0], spawnarea, Quaternion.identity);
                }
                else if (i > 3 && i < 7)
                {
                 
                    Instantiate(currentwave.typeofenemies[1], spawnarea, Quaternion.identity);
                }
                else if (i > 6 && i < 9)
                {
          
                    Instantiate(currentwave.typeofenemies[2], spawnarea, Quaternion.identity);
                }
                else if (i > 8 && i < 10)
                {
                    Instantiate(currentwave.typeofenemies[3], spawnarea, Quaternion.identity);
                }
            }
            currentwave.numberofenemies -= 4;
        }
        else
        {
            enemyspawntime -= Time.deltaTime;
        }
    }
}
