using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public GameObject subtitlePrefab;

    public void DoSubtitle(string subText)
    {
        GameObject newSub = Instantiate(subtitlePrefab);

        // set the instance's parent to our transform and set it as the first (top) sibling
        newSub.transform.parent = transform;
        newSub.transform.SetAsFirstSibling();

        newSub.GetComponent<SubtitleObject>().InitSubtitle(subText);
        
    }
}
