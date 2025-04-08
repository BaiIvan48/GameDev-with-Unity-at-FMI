using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentPoint : MonoBehaviour
{
    public Direction pointDirection;
    public bool isUsed = false;

    private SegmentTemplates templates;
    private SegmentType segmentType;
    private int rand;

    private void Start()
    {
        //Destroy(gameObject, 4f);
        templates = GameObject.FindGameObjectWithTag("PGGameManager").GetComponent<SegmentTemplates>();
        segmentType = gameObject.GetComponentInParent<Segment>().segmentType;
        Invoke("Spawn", 0.1f);
    }

    private void Spawn()
    {
        if (isUsed)
        {
            return;
        }
        else
        {
            switch (pointDirection)
            {
                case Direction.Center:
                    break;

                case Direction.Right:
                    if (segmentType==SegmentType.Spawn)
                    {
                        rand = Random.Range(0,templates.middleSegments.Length);
                        Instantiate(templates.middleSegments[rand], transform.position, Quaternion.identity);
                    }
                    else if (segmentType == SegmentType.Middle)
                    {
                        rand = Random.Range(0, templates.rightUpSegments.Length);
                        Instantiate(templates.rightUpSegments[rand], transform.position, Quaternion.identity);
                    }
                    else if (segmentType == SegmentType.LeftBegin)
                    {
                        rand = Random.Range(0, templates.middleSegments.Length);
                        Instantiate(templates.middleSegments[rand], transform.position, Quaternion.identity);
                    }
                    gameObject.SetActive(false);
                    break;

                case Direction.Left:
                    if (segmentType == SegmentType.Middle)
                    {
                        rand = Random.Range(0, templates.leftUpSegments.Length);
                        Instantiate(templates.leftUpSegments[rand], transform.position, Quaternion.identity);
                    }
                    else if (segmentType == SegmentType.RightBegin)
                    {
                        rand = Random.Range(0, templates.middleSegments.Length);
                        Instantiate(templates.middleSegments[rand], transform.position, Quaternion.identity);
                    }
                    gameObject.SetActive(false);
                    break;

                case Direction.Up:
                    if (segmentType == SegmentType.RightUp)
                    {
                        rand = Random.Range(0, templates.rightBeginSegments.Length);
                        Instantiate(templates.rightBeginSegments[rand], transform.position, Quaternion.identity);
                    }
                    else if (segmentType == SegmentType.LeftUp)
                    {
                        rand = Random.Range(0, templates.leftBeginSegments.Length);
                        Instantiate(templates.leftBeginSegments[rand], transform.position, Quaternion.identity);
                    }
                    gameObject.SetActive(false);
                    break;

                case Direction.Down:
                    gameObject.SetActive(false);
                    break;

                default: Debug.Log("Invalid point direction!");
                    break;
            }
            isUsed = true;


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (other.CompareTag("StartingRoom"))
        //{
        //    spawned = true;
        //    Destroy(gameObject);
        //}
        //else if (other.CompareTag("RoomSpawnPoint"))
        //{
        //    if (!other.GetComponent<RoomSpawner>().spawned && !this.spawned)
        //    {
        //        Instantiate(roomTemplate.closedRoom, transform.position, Quaternion.identity);
        //        Destroy(gameObject);
        //    }
        //    spawned = true;
        //}

        if (collision.CompareTag("SegmentSpawnPoint") && collision.gameObject.GetComponentInParent<Segment>().segmentType==SegmentType.Spawn)
        {
            isUsed = true;
            Destroy(gameObject);
        }
        if (collision.CompareTag("SegmentSpawnPoint"))
        {
            SegmentPoint other = collision.GetComponent<SegmentPoint>();
            if (other != null && other.pointDirection == Direction.Center)
            {
                isUsed = true;
                Destroy(gameObject);
            }
        }
    }
}
