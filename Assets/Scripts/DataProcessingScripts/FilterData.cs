using System;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;

public class FilterData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        char[] spectral_types = { 'O', 'B', 'A', 'F', 'G', 'K', 'M'};
        string allData = System.IO.File.ReadAllText("Assets/Data/hygdata_v3.csv");
        string[] lineData = allData.Split('\n');
        var newFile = System.IO.File.CreateText("Assets/Data/hyg_small.csv");
        string[] firstLine = lineData[0].Split(',');
        newFile.WriteLine("{0},{1},{2},{3},{4},{5}_{6}", firstLine[13], firstLine[15], firstLine[17], firstLine[18], firstLine[19], firstLine[5], firstLine[6]);
        var e = (float)Math.E;
        for (var i = 1; i < lineData.Length-1; i++)
        {
            string[] eachLine = lineData[i].Split(',');
            // 0 - id (has everything); 5,6 - bayerflamsteed designation, proper name (some, some with both); 13 - magnitude (has everything); 15 - spectral type (some, others white); 17,18,19 - x, y, z (has everything)
            string spect = "";

            //if (Single.TryParse(eachLine[13], out float numValue))
            //if (Int32.TryParse(eachLine[0], out int numValue))
            if (eachLine[15].Length!=0)
            {
                if (spectral_types.Contains(eachLine[15][0])) {
                    spect = eachLine[15][0].ToString();
                }
            }
            var mag = Single.Parse(eachLine[13]);
            mag += (26.7f + e); //min is sun with mag of -26.7, make this e
            mag = Mathf.Log(mag);
            mag = Mathf.Round(mag/50 * 100000f) / 100000f;
            string bf_proper = "";
            if (eachLine[6].Length != 0)
            {
                bf_proper = eachLine[6];
            }
            else if (eachLine[5].Length != 0)
            {
                bf_proper = eachLine[5];
            }
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            bf_proper = regex.Replace(bf_proper, " ");
            newFile.WriteLine("{0},{1},{2},{3},{4},{5}", mag, spect, Mathf.Round(float.Parse(eachLine[17]) * 100000f) / 100000f, Mathf.Round(float.Parse(eachLine[18]) * 100000f) / 100000f, Mathf.Round(float.Parse(eachLine[19]) * 100000f) / 100000f, bf_proper); // 1ft should be about a parsec 
        }
        newFile.Close();
    }
}