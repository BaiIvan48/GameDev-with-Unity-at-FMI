using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class SegmentPoint : MonoBehaviour
{
    public Direction pointDirection;
    public bool isUsed = false;

    private SegmentTemplates templates;

    private void Awake()
    {
        templates = GameObject.FindGameObjectWithTag("PGGameManager").GetComponent<SegmentTemplates>();
    }

    public void Spawn(bool nextHasKey)
    {
        if (isUsed)
        {
            gameObject.SetActive(false);
            return;
        }

        SegmentType segmentType = gameObject.GetComponentInParent<Segment>().segmentType;
        GameObject[] segmentArray = GetSegmentArrayForSpawn(pointDirection, segmentType, nextHasKey);

        if (segmentArray != null && segmentArray.Length > 0)
        {
            int rand = Random.Range(0, segmentArray.Length);
            GameObject last = Instantiate(segmentArray[rand], transform.position, Quaternion.identity);
            RemoveColidingPoints(pointDirection, last);
        }

        isUsed = true;
        gameObject.SetActive(false);
    }

    private GameObject[] GetSegmentArrayForSpawn(Direction direction, SegmentType type, bool nextHasKey)
    {
        switch (direction)
        {
            case Direction.Right:
                if (type == SegmentType.Spawn)
                    return templates.middleSegments;
                if (type == SegmentType.Middle || type == SegmentType.MiddleWithKey)
                    return templates.rightUpSegments;
                if (type == SegmentType.LeftBegin && nextHasKey)
                    return templates.middleWithKeySegments;
                return templates.middleSegments;

            case Direction.Left:
                if (type == SegmentType.Spawn)
                    return templates.middleSegments;
                if (type == SegmentType.Middle || type == SegmentType.MiddleWithKey)
                    return templates.leftUpSegments;
                if (type == SegmentType.RightBegin && nextHasKey)
                    return templates.middleWithKeySegments;
                return templates.middleSegments;

            case Direction.Up:
                if (type == SegmentType.RightUp)
                    return templates.rightBeginSegments;
                if (type == SegmentType.LeftUp)
                    return templates.leftBeginSegments;
                return null;

            case Direction.Down:
            case Direction.Center:
            default:
                return null;
        }
    }

    private void RemoveColidingPoints(Direction direction, GameObject last)
    {
        string opposite = direction switch
        {
            Direction.Right => "SegmentLeft",
            Direction.Left => "SegmentRight",
            Direction.Up => "SegmentDown",
            _ => null
        };

        if (opposite != null)
        {
            Transform oppositePoint = last.transform.Find($"SpawnPoints/{opposite}");
            if (oppositePoint != null)
            {
                SegmentPoint sp = oppositePoint.GetComponent<SegmentPoint>();
                if (sp != null) sp.isUsed = true;
            }
        }
    }

    //// For some reason they don't work and that's why i disable the points manually
    //// Disabled collision-based logic:
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("SegmentSpawnPoint"))
    //     {
    //         SegmentPoint other = collision.GetComponent<SegmentPoint>();
    //         if (other != null && other.pointDirection == Direction.Center)
    //         {
    //             isUsed = true;
    //             Destroy(gameObject);
    //         }
    //     }
    // }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("SegmentSpawnPoint"))
    //     {
    //         SegmentPoint other = collision.gameObject.GetComponent<SegmentPoint>();
    //         if (other != null && other.pointDirection == Direction.Center)
    //         {
    //             isUsed = true;
    //             Destroy(gameObject);
    //         }
    //     }
    // }
}
