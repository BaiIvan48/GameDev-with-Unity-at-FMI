using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class SegmentPoint : MonoBehaviour
{
    public Direction pointDirection;
    public bool isUsed = false;

    public void Spawn()
    {
        SegmentTemplates templates = GameObject.FindGameObjectWithTag("PGGameManager").GetComponent<SegmentTemplates>();
        SegmentType segmentType = gameObject.GetComponentInParent<Segment>().segmentType; ;
        int rand;

        if (isUsed)
        {
            gameObject.SetActive(false);
            return;
        }
        else
        {
            switch (pointDirection)
            {
                case Direction.Center:
                    break;

                case Direction.Right:
                    if (segmentType == SegmentType.Spawn)
                    {
                        //GameObject last = CreateNextSegment(templates, templates.middleSegments);
                        rand = Random.Range(0, templates.middleSegments.Length);
                        GameObject last = Instantiate(templates.middleSegments[rand], transform.position, Quaternion.identity);
                        RemoveColadingPoints(pointDirection,last);
                    }
                    else if (segmentType == SegmentType.Middle)
                    {
                        rand = Random.Range(0, templates.rightUpSegments.Length);
                        GameObject last = Instantiate(templates.rightUpSegments[rand], transform.position, Quaternion.identity);
                        RemoveColadingPoints(pointDirection, last);
                    }
                    else if (segmentType == SegmentType.LeftBegin)
                    {
                        rand = Random.Range(0, templates.middleSegments.Length);
                        GameObject last = Instantiate(templates.middleSegments[rand], transform.position, Quaternion.identity);
                        RemoveColadingPoints(pointDirection, last);
                    }

                    gameObject.SetActive(false);
                    break;

                case Direction.Left:
                    if (segmentType == SegmentType.Spawn)
                    {
                        rand = Random.Range(0, templates.middleSegments.Length);
                        GameObject last = Instantiate(templates.middleSegments[rand], transform.position, Quaternion.identity);
                        RemoveColadingPoints(pointDirection, last);
                    }
                    else if (segmentType == SegmentType.Middle)
                    {
                        rand = Random.Range(0, templates.leftUpSegments.Length);
                        GameObject last = Instantiate(templates.leftUpSegments[rand], transform.position, Quaternion.identity);
                        RemoveColadingPoints(pointDirection, last);
                    }
                    else if (segmentType == SegmentType.RightBegin)
                    {
                        rand = Random.Range(0, templates.middleSegments.Length);
                        GameObject last = Instantiate(templates.middleSegments[rand], transform.position, Quaternion.identity);
                        RemoveColadingPoints(pointDirection, last);
                    }
                    gameObject.SetActive(false);
                    break;

                case Direction.Up:
                    if (segmentType == SegmentType.RightUp)
                    {
                        rand = Random.Range(0, templates.rightBeginSegments.Length);
                        GameObject last = Instantiate(templates.rightBeginSegments[rand], transform.position, Quaternion.identity);
                        RemoveColadingPoints(pointDirection, last);
                    }
                    else if (segmentType == SegmentType.LeftUp)
                    {
                        rand = Random.Range(0, templates.leftBeginSegments.Length);
                        GameObject last = Instantiate(templates.leftBeginSegments[rand], transform.position, Quaternion.identity);
                        RemoveColadingPoints(pointDirection, last);
                    }
                    gameObject.SetActive(false);
                    break;

                case Direction.Down:
                    gameObject.SetActive(false);
                    break;

                default:
                    Debug.Log("Invalid point direction!");
                    break;
            }
            isUsed = true;
        }
    }

    //private GameObject CreateNextSegment(SegmentTemplates templates,GameObject[] arr)
    //{
    //    int rand = Random.Range(0, arr.Length);
    //    return Instantiate(arr[rand], transform.position, Quaternion.identity);
    //}

    private void RemoveColadingPoints(Direction direction, GameObject last)
    {

        switch (direction)
        {
            case Direction.Right:
                SegmentPoint r = last.transform.Find("SpawnPoints/SegmentLeft").GetComponent<SegmentPoint>();
                r.isUsed = true;
                break;

            case Direction.Left:

                SegmentPoint l = last.transform.Find("SpawnPoints/SegmentRight").GetComponent<SegmentPoint>();
                l.isUsed = true;
                break;

            case Direction.Up:
                SegmentPoint u = last.transform.Find("SpawnPoints/SegmentDown").GetComponent<SegmentPoint>();
                u.isUsed = true;
                break;

            case Direction.Down:
                break;

            default:
                break;
        }

        //if (point != null)
        //{
        //    //point.gameObject.SetActive(false);
        //    point.isUsed = true;
        //}
    }

    //////////////////////////////////////////////////////////For some reason they don't work and that's why i disable the points manually
    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //    if (collision.CompareTag("SegmentSpawnPoint"))
    //    {
    //        SegmentPoint other = collision.GetComponent<SegmentPoint>();
    //        if (other != null && other.pointDirection == Direction.Center)
    //        {
    //            isUsed = true;
    //            Destroy(gameObject);
    //        }
    //    }
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("SegmentSpawnPoint"))
    //    {
    //        SegmentPoint other = collision.gameObject.GetComponent<SegmentPoint>();
    //        if (other != null && other.pointDirection == Direction.Center)
    //        {
    //            isUsed = true;
    //            Destroy(gameObject);
    //        }
    //    }
    //}
}
