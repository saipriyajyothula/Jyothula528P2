using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starClipping : MonoBehaviour
{
    int childrenNo;
    Transform t;

    // Start is called before the first frame update
    void Start()
    {
        t = gameObject.transform;
        childrenNo = t.childCount;
        for (var i=0; i<childrenNo; i++)
        {
            if(Vector3.Distance(t.GetChild(i).transform.position, Camera.main.transform.position) > 30)
            {
                t.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                t.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        childrenNo = t.childCount;
        for (var i = 0; i < childrenNo; i++)
        {
            if (Vector3.Distance(t.GetChild(i).transform.position, Camera.main.transform.position) > 30)
            {
                t.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                t.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
