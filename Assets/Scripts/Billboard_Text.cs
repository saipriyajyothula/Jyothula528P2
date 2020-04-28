using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Billboard_Text : MonoBehaviour
{
    public float farDistance = 3f;
    public float nearDistance = 0.5f;
    private MeshRenderer mrenderer;
    public string data;
    public string exo_data;
    public GameObject stars;
    GenerateStars genStars;
    private bool show_exo = false;

    // Start is called before the first frame update
    void Start()
    {
        stars = gameObject.transform.parent.transform.parent.gameObject;
        mrenderer = gameObject.GetComponent<MeshRenderer>();
        genStars = stars.GetComponent<GenerateStars>();
        if (exo_data == "1")
        {
            exo_data = " (" + exo_data + " planet)";
        }
        else if(exo_data!="")
        {
            exo_data = " (" + exo_data + " planets)";
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        var dist = Vector3.Distance(Camera.main.transform.position, transform.position);
        if (dist > farDistance)
        {
            mrenderer.enabled = false;
        }
        else if (dist < nearDistance)
        {
            mrenderer.enabled = false;
        }
        else
        {
            mrenderer.enabled = true;
        }
        if (exo_data != "")
        {
            var temp_exo = genStars.show_exo;
            if (temp_exo != show_exo)
            {
                changeTextData(temp_exo);
                show_exo = temp_exo;
            }
        }
    }

    public void changeTextData(bool i)
    {
        if(!i)
        {
            gameObject.GetComponent<TextMeshPro>().text = data;
        }
        else
        {
            gameObject.GetComponent<TextMeshPro>().text = data + exo_data;
        }
    }
}
