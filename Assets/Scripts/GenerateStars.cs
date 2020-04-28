using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateStars : MonoBehaviour
{
    public GameObject starPrefab;
    public GameObject textPrefab;
    public TextAsset dataFile;
    public bool show_exo = false;
    //public GameObject sceneCamera;

    private int starCount = 1;
    private float e = 2.71828f;
    public Color[] colorArray;
    Dictionary<string, int> spectral_types = new Dictionary<string, int>() { { "O", 0 }, { "B", 1 }, { "A", 2 }, { "F", 3 }, { "G", 4 }, { "K", 5 }, { "M", 6 } };
    // Start is called before the first frame update
    void Start()
    {
        string allData = dataFile.text;
        string[] lineData = allData.Split('\n');
        for (var i = 1; i < lineData.Length - 1; i++)
        {
            string[] eachLine = lineData[i].Split(',');
            var star = Instantiate(starPrefab, new Vector3(float.Parse(eachLine[2]), float.Parse(eachLine[3]), float.Parse(eachLine[4])), Quaternion.identity, gameObject.transform);
            star.name = "S" + starCount;
            starCount++;
            var scale = float.Parse(eachLine[0]);
            star.transform.localScale = new Vector3(scale, scale, scale);
            if (eachLine[1].Length != 0)
            {
                star.GetComponent<Renderer>().material.SetColor("_Color", colorArray[spectral_types[eachLine[1]]]);
            }
            if (eachLine[5].Length != 0)
            {
                var tmpText = Instantiate(textPrefab, star.transform);
                tmpText.GetComponent<TextMeshPro>().text = eachLine[5];
                if (starCount == 2)
                {
                    tmpText.GetComponent<Billboard_Text>().nearDistance = 0.1f;
                    tmpText.GetComponent<Billboard_Text>().farDistance = 5f;
                }
                tmpText.GetComponent<Billboard_Text>().data = eachLine[5];
                tmpText.GetComponent<Billboard_Text>().exo_data = eachLine[6];
                //tmpText.GetComponent<Text_Camera>().sceneCamera = sceneCamera;
            }
            else if(eachLine[6].Length != 0)
            {
                //Debug.Log(i);
                var tmpText = Instantiate(textPrefab, star.transform);
                tmpText.GetComponent<TextMeshPro>().text = eachLine[5];
                tmpText.GetComponent<Billboard_Text>().data = eachLine[5];
                tmpText.GetComponent<Billboard_Text>().exo_data = eachLine[6];
            }
        }
    }

    public void changeExo(bool i)
    {
        show_exo = i;
    }
}