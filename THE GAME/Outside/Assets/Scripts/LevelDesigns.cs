using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesigns : MonoBehaviour
{
	public Texture2D level01;
	public Texture2D level02;
	public Texture2D level03;

	private Texture2D currentLevel;

	public Color32[,] getLevelDesign (int level)
	{
		if (level == 1)
			currentLevel = level01;
		else if (level == 2)
			currentLevel = level02;
		else if (level == 3)
			currentLevel = level03;
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
