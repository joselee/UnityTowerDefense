using UnityEngine;
using System.Collections;

public class GUIButton{
	private GUIProperties properties;
	private bool hidden = true;

	public GUIButton(GUIProperties properties)
	{
		this.properties = properties;
	}

	public GUIProperties GetProperties()
	{
		return this.properties;
	}

	public void Hide()
	{
		this.hidden = true;
	}

	public void Show()
	{
		this.hidden = false;
	}

	public bool IsHidden()
	{
		return this.hidden;
	}
}

