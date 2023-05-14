using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNextWave : MonoBehaviour
{
    [SerializeField]
    public GameObject CurrentWave;
    public GameObject NextWave;


    private void Update()
    {
        // Check if there are no active enemies in Wave 1
        if (HasNoActiveEnemies(CurrentWave))
        {
            // Activate Wave 2
            NextWave.SetActive(true);
        }
    }

    private bool HasNoActiveEnemies(GameObject wave)
    {
        // Iterate over the child objects of the wave
        for (int i = 0; i < wave.transform.childCount; i++)
        {
            GameObject enemy = wave.transform.GetChild(i).gameObject;

            // Check if the enemy is active
            if (enemy.activeSelf)
            {
                // Return false if at least one enemy is active
                return false;
            }
        }

        // Return true if no active enemies found
        return true;
    }

}
