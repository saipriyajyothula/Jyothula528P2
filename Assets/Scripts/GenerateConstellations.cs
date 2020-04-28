using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateConstellations : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject textPrefab;
    public TextAsset[] dataFile;

    // Start is called before the first frame update
    void Start()
    {
        createConstellations(0);
    }

    public void createConstellations(int sc)
    {
        if (transform.GetChild(sc).childCount != 0)
        {
            for(var i = 0; i < transform.childCount; i++)
            {
                if (i == sc)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
        else
        {
            string allData = dataFile[sc].text;
            string[] lineData = allData.Split('\n');
            GameObject mainConst = transform.GetChild(sc).gameObject;
            for (var k = 0; k < transform.childCount; k++)
            {
                if (k == sc)
                {
                    transform.GetChild(k).gameObject.SetActive(true);
                }
                else
                {
                    transform.GetChild(k).gameObject.SetActive(false);
                }
            }
            for (var i = 0; i < lineData.Length - 1; i++)
            {
                string[] eachLine = lineData[i].Split(',');
                var constellation = new GameObject();
                constellation.name = eachLine[0];
                constellation.transform.parent = mainConst.transform;
                for (var j = 1; j <= (eachLine.Length - 1) / 6; j++)
                {
                    var line1 = Instantiate(linePrefab, constellation.transform);
                    var line1Renderer = line1.GetComponent<LineRenderer>();
                    line1Renderer.positionCount = 2;
                    line1Renderer.SetPosition(0, new Vector3(float.Parse(eachLine[(j - 1) * 6 + 1]), float.Parse(eachLine[(j - 1) * 6 + 2]), float.Parse(eachLine[(j - 1) * 6 + 3])));
                    line1Renderer.SetPosition(1, new Vector3(float.Parse(eachLine[(j - 1) * 6 + 4]), float.Parse(eachLine[(j - 1) * 6 + 5]), float.Parse(eachLine[(j - 1) * 6 + 6])));
                }
                //var tmpText = Instantiate(textPrefab, new Vector3(float.Parse(eachLine[eachLine.Length - 3]), float.Parse(eachLine[eachLine.Length - 2]), float.Parse(eachLine[eachLine.Length - 1])), Quaternion.identity, constellation.transform);
                //var tmpText = Instantiate(textPrefab, constellation.transform);
                //tmpText.GetComponent<TextMeshPro>().text = eachLine[0];
            }
        }
    }

    public void hideConstellations()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
