using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFoliage : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPointLeft;
    [SerializeField]
    private GameObject spawnPointRight;

    [SerializeField]
    private GameObject spawnPointGrass1;
    [SerializeField]
    private GameObject spawnPointGrass2;
    [SerializeField]
    private GameObject spawnPointGrass3;
    [SerializeField]
    private GameObject spawnPointGrass4;
    [SerializeField]
    private GameObject spawnPointGrass5;

    private int childrenLeft = 0;
    private int childrenRight = 0;
    
    private int childrenGrass1 = 0;
    private int childrenGrass2 = 0;
    private int childrenGrass3 = 0;
    private int childrenGrass4 = 0;
    private int childrenGrass5 = 0;


    private void Start()
    {
        childrenLeft = spawnPointLeft.transform.childCount;
        childrenRight = spawnPointRight.transform.childCount;

        SpawnFoliageForObject(spawnPointLeft, childrenLeft); 
        SpawnFoliageForObject(spawnPointRight, childrenRight);

        childrenGrass1 = spawnPointGrass1.transform.childCount;
        childrenGrass2 = spawnPointGrass2.transform.childCount;
        childrenGrass3 = spawnPointGrass3.transform.childCount;
        childrenGrass4 = spawnPointGrass4.transform.childCount;
        childrenGrass5 = spawnPointGrass5.transform.childCount;

        SpawnFoliageForObject(spawnPointGrass1, childrenGrass1);
        SpawnFoliageForObject(spawnPointGrass2, childrenGrass2);
        SpawnFoliageForObject(spawnPointGrass3, childrenGrass3);
        SpawnFoliageForObject(spawnPointGrass4, childrenGrass4);
        SpawnFoliageForObject(spawnPointGrass5, childrenGrass5);
    }
    void SpawnFoliageForObject(GameObject spawn_point, int child_range)
    {
        int child_id = Random.Range(0, child_range);
        spawn_point.transform.GetChild(child_id).gameObject.SetActive(true);
    }
}

