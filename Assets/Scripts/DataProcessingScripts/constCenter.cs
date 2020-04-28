using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class constCenter : MonoBehaviour
{
    public GameObject constellations;
    bool firstTime = true;
    System.IO.StreamWriter newFile;
    string[] constArray;

    // Start is called before the first frame update
    void Start()
    {
        newFile = System.IO.File.CreateText("Assets/Data/western_const_final.csv");
        string constData = System.IO.File.ReadAllText("Assets/Data/western_const.csv");
        constArray = constData.Split('\n');
    }

    // Update is called once per frame
    void Update()
    {
        if (firstTime)
        {
            if (constellations.transform.childCount > 0)
            {
                for (var i = 0; i < constellations.transform.childCount; i++)
                {
                    Vector3 centerVal = Vector3.zero;
                    for (var j = 0; j < constellations.transform.GetChild(i).childCount; j++)
                    {
                        centerVal += constellations.transform.GetChild(i).GetChild(j).GetComponent<Renderer>().bounds.center;
                    }
                    centerVal /= constellations.transform.GetChild(i).childCount;
                    newFile.WriteLine(constArray[i] + ',' + centerVal[0] + ',' + centerVal[1] + ',' + centerVal[2]);
                }
                firstTime = false;
                newFile.Close();
            }
        }
    }
}