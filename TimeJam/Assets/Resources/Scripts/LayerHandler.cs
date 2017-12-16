using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerHandler : MonoBehaviour
{
	private enum Layer
	{
		Past,
		Present,
		Future
	}

	public GameObject layer_past;
	public GameObject layer_present;
	public GameObject layer_future;

	// Use this for initialization
	void Start ()
	{
		SetActiveLayer(Layer.Present);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			SetActiveLayer(Layer.Past);
		}
		else if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			SetActiveLayer(Layer.Present);
		}
		else if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			SetActiveLayer(Layer.Future);
		}
	}

	private void SetActiveLayer(Layer layer)
	{
		switch(layer)
		{
			case Layer.Past:
				layer_past.SetActive(true);
				layer_present.SetActive(false);
				layer_future.SetActive(false);
				break;
			case Layer.Present:
				layer_past.SetActive(false);
				layer_present.SetActive(true);
				layer_future.SetActive(false);
				break;
			case Layer.Future:
				layer_past.SetActive(false);
				layer_present.SetActive(false);
				layer_future.SetActive(true);
				break;

		}

	}
}
