using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchMode : MonoBehaviour
{
    [SerializeField]
    public GameObject[] gos;

    public void switchData(int i)
	{
		for(var k=0; k<gos.Length; k++)
		{
			if (i == k)
			{
				gos[k].gameObject.SetActive(true);
			}
			else
			{
				gos[k].gameObject.SetActive(false);
			}
		}
    }
}
