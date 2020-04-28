using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateTychoStars : MonoBehaviour
{
    public TextAsset dataFile;
    public ParticleSystem ps;
    //public GameObject sceneCamera;

    private float e = 2.71828f;
    public Color[] colorArray;
    Dictionary<string, int> spectral_types = new Dictionary<string, int>() { { "O", 0 }, { "B", 1 }, { "A", 2 }, { "F", 3 }, { "G", 4 }, { "K", 5 }, { "M", 6 } };
    string allData;
    string[] lineData;
    // Start is called before the first frame update
    void Start()
    {
        allData = dataFile.text;
        lineData = allData.Split('\n');
        var emitParams = new ParticleSystem.EmitParams();
        for (var i = 1; i < lineData.Length - 1; i++)
        {
            string[] eachLine = lineData[i].Split(',');
            float scale = float.Parse(eachLine[0])+26.7f+e;
            scale = Mathf.Log(scale);
            scale = Mathf.Round(scale / 10 * 100000f) / 100000f;
            Color c = new Color(1, 1, 0.9372549f, 1);
            if (eachLine[1].Length != 0)
            {
                try
                {
                    c = colorArray[spectral_types[eachLine[1][0].ToString()]];
                }
                catch
                {

                }
            }
            emitParams.position = new Vector3(float.Parse(eachLine[2]), float.Parse(eachLine[3]), float.Parse(eachLine[4]));
            emitParams.startColor = c;
            emitParams.startSize = scale;
            emitParams.startLifetime = Mathf.Infinity;
            emitParams.velocity = Vector3.zero;
            ps.Emit(emitParams, 1);
        }
    }

    private void Update()
    {
        if (ps.particleCount == 0)
        {
            var emitParams = new ParticleSystem.EmitParams();
            for (var i = 1; i < lineData.Length - 1; i++)
            {
                string[] eachLine = lineData[i].Split(',');
                float scale = float.Parse(eachLine[0]) + 26.7f + e;
                scale = Mathf.Log(scale);
                scale = Mathf.Round(scale / 10 * 100000f) / 100000f;
                Color c = new Color(1, 1, 0.9372549f, 1);
                if (eachLine[1].Length != 0)
                {
                    try
                    {
                        c = colorArray[spectral_types[eachLine[1][0].ToString()]];
                    }
                    catch
                    {

                    }
                }
                emitParams.position = new Vector3(float.Parse(eachLine[2]), float.Parse(eachLine[3]), float.Parse(eachLine[4]));
                emitParams.startColor = c;
                emitParams.startSize = scale;
                emitParams.startLifetime = Mathf.Infinity;
                emitParams.velocity = Vector3.zero;
                ps.Emit(emitParams, 1);
            }
        }
    }
}
