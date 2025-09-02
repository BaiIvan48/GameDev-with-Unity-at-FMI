using UnityEngine;

public class Segment : MonoBehaviour
{
    public SegmentType segmentType;
    private SegmentTemplates templates;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("PGGameManager").GetComponent<SegmentTemplates>();
        templates.segmentsInLevel.Add(this.gameObject);
    }
}
