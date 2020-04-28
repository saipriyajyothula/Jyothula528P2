using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starLOD : MonoBehaviour
{
    MeshRenderer mrenderer;
    // Start is called before the first frame update
    void Start()
    {
        mrenderer = gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(gameObject.transform.position, Camera.main.transform.position) > 100)
        {
            mrenderer.enabled = false;
        }
        else
        {
            mrenderer.enabled = true;
        }
    }
}
