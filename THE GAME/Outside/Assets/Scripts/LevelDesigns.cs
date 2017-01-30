using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesigns : MonoBehaviour {
	public Texture2D testLevelImg;
	public Color32[,] getLevelDesign(int level) {
		if (level == 0) {
			Color32 [,] levelArray = new Color32[testLevelImg.width,testLevelImg.height];
			for (int i = 0; i < testLevelImg.width; i++) {
				for (int j = 0; j < testLevelImg.height; j++) {
					Color32 currentPixelColor = testLevelImg.GetPixel (i, j);
					levelArray [i, j] = currentPixelColor;
				}
			}
			return levelArray;
		}
		return null;
	}
		
}
