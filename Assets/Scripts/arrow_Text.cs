using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class arrow_Text : MonoBehaviour
{
    public GameObject text;
    public GameObject arrow;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        arrow.transform.LookAt(Vector3.zero);
        text.transform.rotation = Camera.main.transform.rotation;
        text.GetComponent<TextMeshPro>().text = "Distance from sun: " + Vector3.Magnitude(gameObject.transform.position) + " parsecs";
    }
}
