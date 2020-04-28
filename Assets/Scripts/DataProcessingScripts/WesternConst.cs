using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;

public class WesternConst : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //string allData = System.IO.File.ReadAllText("Assets/Data/hygdata_v3.csv");
        //string[] lineData = allData.Split('\n');
        //var newFile = System.IO.File.CreateText("Assets/Data/hip_data.csv");
        //for (var i = 1; i < lineData.Length - 1; i++)
        //{
        //    string[] eachLine = lineData[i].Split(',');
        //    newFile.WriteLine(eachLine[1]);
        //}
        //newFile.Close();

        string smallData = System.IO.File.ReadAllText("Assets/Data/hyg_small.csv");
        string hipData = System.IO.File.ReadAllText("Assets/Data/hip_data.csv");
        string namesData = System.IO.File.ReadAllText("Assets/Data/romanian_constellation_names.fab");
        string sticksData = System.IO.File.ReadAllText("Assets/Data/romanian_constellationship.fab");

        string[] hipArray = hipData.Split('\n');
        string[] smallArray = smallData.Split('\n');
        string[] namesArray = namesData.Split('\n');
        string[] sticksArray = sticksData.Split('\n');

        Dictionary<string, string> constellation_map = new Dictionary<string, string>();
        Dictionary<string, string> hip_map = new Dictionary<string, string>();
        RegexOptions options = RegexOptions.None;
        Regex regex = new Regex("[ ]{2,}", options);

        for (var k = 0; k < namesArray.Length - 1; k++)
        {
            string eachLine = namesArray[k];
            string[] eachLineArray = eachLine.Split('"');
            //Debug.Log(eachLineArray[0].Split('\t')[0] + ", " + eachLineArray[1]);
            constellation_map.Add(eachLineArray[0].Split('\t')[0].Split(' ')[0], eachLineArray[1]);
        }

        for (var k = 0; k < hipArray.Length; k++)
        {
            if (hipArray[k] != "")
            {
                try
                {
                    hip_map.Add(hipArray[k], (k+1).ToString());
                }
                catch
                {

                }
            }
        }

        var newFile = System.IO.File.CreateText("Assets/Data/romanian_const.csv");
        for (var i = 0; i < sticksArray.Length - 3; i++)
        {

            string eachStick = sticksArray[i];
            eachStick = regex.Replace(eachStick, " ");
            string[] eachStickArray = eachStick.Split(new char[] { ' ' , '\t'});
            var str = "";
            for (var j = 0; j < eachStickArray.Length; j++)
            {
                //str += eachStickArray[j];
                //str += ", ";
                if (j == 0)
                {
                    str = constellation_map[eachStickArray[j]] + ",";
                }
                else if (j == eachStickArray.Length - 1)
                {
                    var smallLine = smallArray[Int32.Parse(hip_map[Int32.Parse(eachStickArray[j]).ToString()])].Split(',');
                    str += (smallLine[2] + "," + smallLine[3] + "," + smallLine[4]);
                }
                else if (j > 1)
                {
                    try
                    {
                        var smallLine = smallArray[Int32.Parse(hip_map[eachStickArray[j]])].Split(',');
                        str += (smallLine[2] + "," + smallLine[3] + "," + smallLine[4] + ",");
                    }
                    catch
                    {
                        Debug.Log(eachStickArray[j]);
                    }
                }
            }
            newFile.WriteLine(str);
        }
        newFile.Close();
    }

}
