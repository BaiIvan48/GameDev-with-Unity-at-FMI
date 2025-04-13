using UnityEngine;

public class PGGameManager : MonoBehaviour
{
    public Transform startPointL;
    public Transform startPointR;
    public SegmentSpawner segmentSpawner;
    public SegmentTemplates templates;

    public GameObject playerPrefab;
    public GameObject cameraObject;

    public int initialHearts = 5;
    public int requiredKeys = 5;

    private void Start()
    {
        bool startFromLeft = (Random.Range(0, 2) == 0);
        Transform chosenStart = startFromLeft ? startPointL : startPointR;
        Transform otherStart = startFromLeft ? startPointR : startPointL;

        GameObject spawnSegment = segmentSpawner.SpawnStartSegment(chosenStart, startFromLeft);
        otherStart.gameObject.SetActive(false);

        GameObject playerInstance = SetupPlayerAndCamera(spawnSegment);
        SetupInitialStats(playerInstance);

        StartCoroutine(segmentSpawner.GenerateLevel(spawnSegment));
    }

    private GameObject SetupPlayerAndCamera(GameObject segment)
    {
        Transform respawnTransform = segment.transform.Find("Environment/RespawnPoint");
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

            SetupInitialStats(playerInstance);

            return playerInstance;
        }
        else
        {
            Debug.LogWarning("No Respawn point found in spawned segment.");
            return null;
        }
    }

    private void SetupInitialStats(GameObject player)
    {
        Health health = player.GetComponent<Health>();
        Pickup pickup = player.GetComponent<Pickup>();

        Transform canvas = GameObject.Find("Canvas").transform;

        DisplayIconCount heartDisplay = canvas.Find("Hearts").GetComponent<DisplayIconCount>();
        DisplayIconCount keyDisplay = canvas.Find("Keys").GetComponent<DisplayIconCount>();

        heartDisplay.SetStat(health);
        keyDisplay.SetStat(pickup);

        keyDisplay.SetIconCount(LevelDificulty.selectedLevelDificulty);
    }
}
