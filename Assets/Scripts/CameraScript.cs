using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraScript : MonoBehaviour
{
	public Shader DrawAsSolidColor;
	public Shader Outline;
	Material _outlineMaterial;
	Camera TempCam;
	float[] kernel;
	void Start()
	{
		_outlineMaterial = new Material(Outline);
		TempCam = new GameObject().AddComponent<Camera>();

		kernel = GaussianKernel.Calculate(5, 21);
	}

	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		TempCam.CopyFrom(Camera.current);
		TempCam.backgroundColor = Color.black;
		TempCam.clearFlags = CameraClearFlags.Color;

		TempCam.cullingMask = 1 << LayerMask.NameToLayer("Outline");

		var rt = RenderTexture.GetTemporary(src.width, src.height, 0, RenderTextureFormat.R8);
		TempCam.targetTexture = rt;

		TempCam.RenderWithShader(DrawAsSolidColor, "");

		_outlineMaterial.SetFloatArray("kernel", kernel);
		_outlineMaterial.SetInt("_kernelWidth", kernel.Length);
		_outlineMaterial.SetTexture("_SceneTex", src);

		//No need for more than 1 sample, which also makes the mask a little bigger than it should be.
		rt.filterMode = FilterMode.Point;

		Graphics.Blit(rt, dst, _outlineMaterial);
		TempCam.targetTexture = src;
		RenderTexture.ReleaseTemporary(rt);
	}
}

//http://haishibai.blogspot.com/2009/09/image-processing-c-tutorial-4-gaussian.html
public static class GaussianKernel
{
	public static float[] Calculate(double sigma, int size)
	{
		float[] ret = new float[size];
		double sum = 0;
		int half = size / 2;
		for (int i = 0; i < size; i++)
		{
			ret[i] = (float)(1 / (Math.Sqrt(2 * Math.PI) * sigma) * Math.Exp(-(i - half) * (i - half) / (2 * sigma * sigma)));
			sum += ret[i];
		}
		return ret;
	}
}