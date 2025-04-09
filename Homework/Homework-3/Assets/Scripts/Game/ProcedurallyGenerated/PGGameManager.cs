using System.Collections;
using UnityEngine;


public class PGGameManager : MonoBehaviour
{
    //public Transform startPointL;
    //public Transform startPointR;
    //public SegmentTemplates templates;
    //public GameObject player;
    //public GameObject Camera;

    //private List<string> levelPlan = new List<string>()
    //{
    //"Spawn",
    //"Middle",
    //"RightUp",
    //"RightBegin",
    //"Middle",
    //"LeftUp",
    //"LeftBegin",
    //"Middle",
    //"RightUp",
    //"RightBegin",
    //"Middle",
    //"LeftUp",
    //"LeftBegin",
    //"MiddleWithKey"
    //};

    //void Start()
    //{
    //    int levelType = LevelDificulty.selectedLevelDificulty;
    //    Debug.Log("Dificulty: " + levelType);

    //    Transform chosenStartPoint = (Random.Range(0, 2) == 0) ? startPointL : startPointR;
    //    Transform otherStartPoint = (chosenStartPoint == startPointL) ? startPointR : startPointL;

    //    GameObject spawn = Instantiate(templates.spawnSegment, chosenStartPoint.position, Quaternion.identity);
    //    templates.segmentsInLevel.Add(spawn);

    //    Transform respawnTransform = null;
    //    foreach (Transform child in spawn.GetComponentsInChildren<Transform>())
    //    {
    //        if (child.CompareTag("Respawn"))
    //        {
    //            respawnTransform = child;
    //            break;
    //        }
    //    }

    //    if (respawnTransform != null)
    //    {
    //        GameObject playerInstance = Instantiate(player, respawnTransform.position, Quaternion.identity);
    //        Camera.transform.position = new Vector3(respawnTransform.position.x, respawnTransform.position.y, -10);

    //        CameraFollow camFollow = Camera.GetComponent<CameraFollow>();
    //        if (camFollow != null)
    //        {
    //            camFollow.enabled = true;
    //            camFollow.SetPlayer(playerInstance);
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogWarning("No Respawn point found in spawned segment.");
    //    }

    //    chosenStartPoint.gameObject.SetActive(false);
    //    otherStartPoint.gameObject.SetActive(false);
    //}

    public Transform startPointL;
    public Transform startPointR;
    public SegmentSpawner segmentSpawner;
    public SegmentTemplates templates;

    public GameObject playerPrefab;
    public GameObject cameraObject;

    private void Start()
    {
        bool startFromLeft = (Random.Range(0, 2) == 0);
        Transform chosenStart = startFromLeft ? startPointL : startPointR;
        Transform otherStart = startFromLeft ? startPointR : startPointL;

        GameObject spawnSegment = segmentSpawner.SpawnStartSegment(chosenStart, startFromLeft);
        otherStart.gameObject.SetActive(false);

        //SetupPlayerAndCamera(spawnSegment);

        StartCoroutine(segmentSpawner.GenerateLevel(spawnSegment));
    }

    private void SetupPlayerAndCamera(GameObject segment)
    {
        Transform respawnTransform = segment.transform.Find("Respawn");
        if (respawnTransform != null)
        {
            GameObject playerInstance = Instantiate(playerPrefab, respawnTransform.position, Quaternion.identity);
            cameraObject.transform.position = new Vector3(respawnTransform.position.x, respawnTransform.position.y, -10);

            CameraFollow camFollow = cameraObject.GetComponent<CameraFollow>();
            if (camFollow != null)
            {
                camFollow.enabled = true;
                camFollow.SetPlayer(playerInstance);
            }
        }
        else
        {
            Debug.LogWarning("No Respawn point found in spawned segment.");
        }
    }
}
