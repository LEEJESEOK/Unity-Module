using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadObjects : MonoBehaviour
{
    List<Transform> childObjects;
    // Start is called before the first frame update
    void Start()
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; ++i)
        {
            childObjects.Add(transform.GetChild(i));
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
