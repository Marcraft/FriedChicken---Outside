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
