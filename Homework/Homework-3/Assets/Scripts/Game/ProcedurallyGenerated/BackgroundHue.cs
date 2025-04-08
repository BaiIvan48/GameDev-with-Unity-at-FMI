using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundHue : MonoBehaviour
{

    void Start()
    {
        ApplyLevelColors();
    }

    void ApplyLevelColors()
    {
        for (int i = 1; i <= 5; i++)
        {
            string levelName = "Level" + i;
            Transform level = transform.Find(levelName);
            if (level != null)
            {
                Color? colorToApply = GetColorForLevel(i);
                if (colorToApply != null)
                {
                    foreach (Transform child in level)
                    {
                        SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
                        if (sr != null)
                        {
                            sr.color = colorToApply.Value;
                        }
                    }
                }
            }
            else
            {
                Debug.LogWarning($"No object found with name: {levelName}");
            }
        }
    }

    Color? GetColorForLevel(int level)
    {
        switch (level)
        {
            case 1: return new Color(0.8f, 1f, 0.8f);    // green
            case 2: return null;                         // blue
            case 3: return new Color(1f, 1f, 0.85f);       // yellow
            case 4: return new Color(1f, 0.6f, 0.3f);     // orange
            case 5: return new Color(1f, 0.3f, 0.3f);     // red
            default: return null;
        }
    }
}
