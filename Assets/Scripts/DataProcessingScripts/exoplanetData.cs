using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class exoplanetData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string exoData = System.IO.File.ReadAllText("Assets/Data/planets_2020.04.26_22.03.56.csv");
        string smallData = System.IO.File.ReadAllText("Assets/Data/hyg_small.csv");
        var newFile = System.IO.File.CreateText("Assets/Data/hyg_small_exo.csv");
        string hipData = System.IO.File.ReadAllText("Assets/Data/hip_data.csv");
        string[] exoLine = exoData.Split('\n');
        string[] lineData = hipData.Split('\n');
        string[] smalllineData = smallData.Split('\n');
        Dictionary<string, int> exo_map = new Dictionary<string, int>();
        for (var i = 1; i < exoLine.Length - 1; i++)
        {
            string[] eachExoLine = exoLine[i].Split(',');
            if (eachExoLine[2] != "")
            {
                try
                {
                    exo_map.Add(eachExoLine[2].Split(' ')[1], Int32.Parse(eachExoLine[1]));
                    //Debug.Log(eachExoLine[2]+","+ eachExoLine[1]);
                }
                catch
                {

                }
            }
        }
        newFile.WriteLine(smalllineData[0]+",exop_no");
        for (var i = 1; i < smalllineData.Length - 1; i++)
        {
            try
            {
                int p_no = exo_map[lineData[i-1]];
                //Debug.Log(p_no +", " + lineData[i-1]);
                newFile.WriteLine(smalllineData[i] + "," + p_no);
            }
            catch
            {
                newFile.WriteLine(smalllineData[i] + ",");
            }
        }
        newFile.Close();
    }
}
