using UnityEngine;
using System.Collections;

public class GUIProperties  {
	private string position = "center-bottom";
	private string textureResource;
	private Vector3 dimensions;
	private IClick clickHandler;

	public GUIProperties()
	{

	}

	public GUIProperties(string position)
	{
		this.position = position;
	}


	public void SetCenterBottom()
	{
		this.position = "center-bottom";
	}

	public void setDimensions(int x, int y)
	{
		this.dimensions = new Vector2(x, y);
	}

	public void SetTexture(string textureResource)
	{
		this.textureResource = textureResource;
	}

	public void SetClickHandler(IClick click)
	{
		this.clickHandler = click;
	}

	public IClick GetClickHandler()
	{
		return this.clickHandler;
	}

	public string GetTextureResource()
	{
		return this.textureResource;
	}
}
