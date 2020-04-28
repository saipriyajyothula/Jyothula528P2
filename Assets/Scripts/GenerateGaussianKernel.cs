using UnityEngine;
using System;

public class GenerateGaussianKernel : MonoBehaviour
{
	public Material starMat;
	private float[] kernel = new float[21];

	// Start is called before the first frame update
	void Start()
	{
		kernel = GaussianKernel.Calculate(5, 21);
        //string str = "";
        //      for(var i=0; i<kernel.Length; i++)
        //      {
        //	str += (kernel[i] + ", ");
        //      }
        //Debug.Log(str);
        //starMat.SetFloatArray("_kernel", kernel);
        //starMat.SetInt("_kernelWidth", kernel.Length);
    }

	//void Update()
	//{
	//	//starMat.SetFloatArray("_kernel", kernel);
	//	//starMat.SetInt("_kernelWidth", kernel.Length);
	//}
}