using System.Collections.Generic;
using UnityEngine;

public class SegmentTemplates : MonoBehaviour
{
    public GameObject spawnSegment;
    public GameObject[] middleSegments;
    public GameObject[] rightUpSegments;
    public GameObject[] leftUpSegments;
    public GameObject[] rightBeginSegments;
    public GameObject[] leftBeginSegments;
    public GameObject middleWithKeySegments;

    public List<GameObject> segmentsInLevel = new List<GameObject>();
}
