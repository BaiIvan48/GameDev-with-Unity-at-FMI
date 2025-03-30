using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFoliage : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPointLeft;
    [SerializeField]
    private GameObject spawnPointRight;

    private int childrenLeft = 0;
    private int childrenRight = 0;

    private void Start()
    {
        childrenLeft = spawnPointLeft.transform.childCount;
        childrenRight = spawnPointRight.transform.childCount;

        SpawnFoliageForObject(spawnPointLeft, childrenLeft); 
        SpawnFoliageForObject(spawnPointRight, childrenRight);
    }
    void SpawnFoliageForObject(GameObject spawn_point, int child_range)
    {
        int child_id = Random.Range(0, child_range);
        spawn_point.transform.GetChild(child_id).gameObject.SetActive(true);
    }
}

