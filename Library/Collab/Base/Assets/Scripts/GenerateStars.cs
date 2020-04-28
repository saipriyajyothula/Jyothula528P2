using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateStars : MonoBehaviour
{
    public GameObject starPrefab;
    public GameObject textPrefab;
    public TextAsset dataFile;
    //public GameObject sceneCamera;

    private int starCount = 1;
    private float e = 2.71828f;
    Dictionary<string, Color> spectral_types = new Dictionary<string, Color>() { { "O", new Color(1, 0.7490196f, 0.7490196f, 1) }, { "B", new Color(1, 0.8117647f, 0.8117647f, 1) }, { "A", new Color(1, 0.8745098f, 0.8745098f, 1) }, { "F", new Color(1, 0.9372549f, 0.9372549f, 1) }, { "G", new Color(0.8745098f, 1, 1, 1) }, { "K", new Color(0.8745098f, 0.8745098f, 1, 1) }, { "M", new Color(0.5607843f, 0.7490196f, 1, 1) } };
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
                star.GetComponent<Renderer>().material.SetColor("_Color", spectral_types[eachLine[1]]);
            }
            if (eachLine[5].Length != 0)
            {
                var tmpText = Instantiate(textPrefab, star.transform);
                tmpText.GetComponent<TextMeshPro>().text = eachLine[5];
                //tmpText.GetComponent<Text_Camera>().sceneCamera = sceneCamera;
            }
        }
    }
}