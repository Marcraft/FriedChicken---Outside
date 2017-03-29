using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesigns : MonoBehaviour
{
	public Texture2D level101;
	public Texture2D level102;
	public Texture2D level103;
	public Texture2D level104;
	public Texture2D level105;
	public Texture2D level106;
	public Texture2D level107;
	public Texture2D level108;
	public Texture2D level109;
	public Texture2D level110;

	public Texture2D level201;
	public Texture2D level202;
	public Texture2D level203;
	public Texture2D level204;
	public Texture2D level205;
	public Texture2D level206;
	public Texture2D level207;
	public Texture2D level208;
	public Texture2D level209;
	public Texture2D level210;

	private Texture2D currentLevel;

	public Color32[,] getLevelDesign (int level)
	{
		if (level == 101)
			currentLevel = level101;
		else if (level == 102)
			currentLevel = level102;
		else if (level == 103)
			currentLevel = level103;
		else if (level == 104)
			currentLevel = level104;
		else if (level == 105)
			currentLevel = level105;
		else if (level == 106)
			currentLevel = level106;
		else if (level == 107)
			currentLevel = level107;
		else if (level == 108)
			currentLevel = level108;
		else if (level == 109)
			currentLevel = level109;
		else if (level == 110)
			currentLevel = level110;
		else if (level == 201)
			currentLevel = level201;
		else if (level == 202)
			currentLevel = level202;
		else if (level == 203)
			currentLevel = level203;
		else if (level == 204)
			currentLevel = level204;
		else if (level == 205)
			currentLevel = level205;
		else if (level == 206)
			currentLevel = level206;
		else if (level == 207)
			currentLevel = level207;
		else if (level == 208)
			currentLevel = level208;
		else if (level == 209)
			currentLevel = level209;
		else if (level == 210)
			currentLevel = level210;
		Color32[,] levelArray = new Color32[currentLevel.width, currentLevel.height];
		for (int i = 0; i < currentLevel.width; i++) {
			for (int j = 0; j < currentLevel.height; j++) {
				Color32 currentPixelColor = currentLevel.GetPixel (i, j);
				levelArray [i, j] = currentPixelColor;
			}
		}
		return levelArray;
	}
		
}
