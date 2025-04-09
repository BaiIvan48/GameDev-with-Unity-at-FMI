using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Linq;

public class SegmentSpawner : MonoBehaviour
{
    public SegmentTemplates templates;
    private bool startLeft = false;

    private GameObject firstSpawnedSegment;

    public GameObject SpawnStartSegment(Transform startPoint, bool startFromLeft)
    {
        startLeft = startFromLeft;
        firstSpawnedSegment = Instantiate(templates.spawnSegment, startPoint.position, Quaternion.identity);

        Transform spawnPoints = firstSpawnedSegment.transform.Find("SpawnPoints");
        if (spawnPoints != null)
        {
            string pointToDisable = startLeft ? "SegmentLeft" : "SegmentRight";
            Transform point = spawnPoints.Find(pointToDisable);
            if (point != null)
            {
                point.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning($"No {pointToDisable} point in SpawnPoints in segment {firstSpawnedSegment.name}");
            }
        }
        else
        {
            Debug.LogWarning("SpawnPoints not found in the segment " + firstSpawnedSegment.name);
        }

        startPoint.gameObject.SetActive(false);
        return firstSpawnedSegment;
    }

    public IEnumerator GenerateLevel(GameObject startSegment)
    {
        yield return new WaitForSeconds(0.1f);

        GameObject currentSegment = startSegment;

        //int segmentsToSpawn = (LevelDificulty.selectedLevelDificulty * 15) - 2;
        int segmentsToSpawn = (5 * 15) - 2;

        while (segmentsToSpawn!=0) ////////////////////////////////////////////////////////////////////////////////////////////// trqbva da dobavim tezi s kluchovete
        {
            Debug.Log("Current    " + currentSegment.ToString()); //////////////////////////////////////////////////////////

            foreach (SegmentPoint point in currentSegment.GetComponentsInChildren<SegmentPoint>())
            {
                Debug.Log(currentSegment.ToString() + "   with point    "+point.ToString());//////////////////////////////////////////
                point.Spawn();
            }

            yield return new WaitForSeconds(0.05f);
            currentSegment = templates.segmentsInLevel.Last();
            Debug.Log("next    " + currentSegment.ToString());////////////////////////////////////////////////////

            segmentsToSpawn--;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
