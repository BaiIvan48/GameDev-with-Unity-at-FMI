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

        int currenLevel = LevelDificulty.selectedLevelDificulty;
        int segmentsToSpawn = (currenLevel * 15) - 2;

        int spawned = 1;
        bool nextHasKey = false;

        while (segmentsToSpawn>0)
        {
            nextHasKey = (segmentsToSpawn == 1 || ((currenLevel > 1) && ((segmentsToSpawn == ((15 * (currenLevel - 1)) + 1)))));

            if (spawned == 14)
            {
                spawned = 0;
                currenLevel--;
            }
            else
            {
                spawned++;
            }

            foreach (SegmentPoint point in currentSegment.GetComponentsInChildren<SegmentPoint>())
            {
                point.Spawn(nextHasKey);
            }

            yield return new WaitForSeconds(0.1f);
            currentSegment = templates.segmentsInLevel.Last();

            segmentsToSpawn--;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
