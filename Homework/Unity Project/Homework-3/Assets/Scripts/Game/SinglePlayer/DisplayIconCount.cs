using UnityEngine;

public class DisplayIconCount : MonoBehaviour
{
    private int iconCount = 5;

    [SerializeField]
    private GameObject[] images;

    [SerializeField]
    private GameObject[] placeHolderImages;

    [SerializeField]
    private Stats<int> statOfInterest;

    [SerializeField]
    private Stat stat;

    private void Awake()
    {
        for (int i = 0; i < placeHolderImages.Length; i++)
        {
            placeHolderImages[i].SetActive(i < iconCount);
            images[i].SetActive(i < iconCount);
        }
    }

    private void OnEnable()
    {
        if (statOfInterest != null)
        {
            statOfInterest.valueUpdateNotify += ActiveIconCount;
            ActiveIconCount(statOfInterest.getValue()); 
        }
    }

    private void OnDisable()
    {
        if (statOfInterest != null)
        {
            statOfInterest.valueUpdateNotify -= ActiveIconCount;
        }
    }

    public void SetStat(Stats<int> stat)
    {
        if (statOfInterest != null)
            statOfInterest.valueUpdateNotify -= ActiveIconCount;

        statOfInterest = stat;

        if (statOfInterest != null)
        {
            statOfInterest.valueUpdateNotify += ActiveIconCount;
            ActiveIconCount(statOfInterest.getValue());
        }
    }

    public Stat GetStatType() => stat;
    public int GetIconCount() => iconCount;

    public void SetIconCount(int count)
    {
        iconCount = count;
        for (int i = 0; i < placeHolderImages.Length; i++)
        {
            placeHolderImages[i].SetActive(i < iconCount);
        }
    } 

    void ActiveIconCount(int n)
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].SetActive(i < n);
        }
    }

}
