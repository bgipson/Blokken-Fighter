using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightningTexture : MonoBehaviour {

    private Texture texture;
    private RawImage image;
    public int textureY = 128;
    public int textureX = 20;

	// Use this for initialization
	void Start () {
        image = GetComponent<RawImage>();
        texture = MakeLightningTexture();
        image.texture = texture;
    }
	
	// Update is called once per frame
	void Update () {
        texture = MakeLightningTexture();
        image.texture = texture;
	}

    Texture MakeLightningTexture()
    {
        Texture2D lightning = new Texture2D(textureX, textureY);
        for (int y = 0; y < textureY; y++)
        {
            int rand = Mathf.FloorToInt(Mathf.PerlinNoise(y / ((float) textureY * 100f), Random.value * 100f) * textureX);
            for (int x = 0; x < textureX; x++)
            {
                Color c;
                if (Mathf.Abs(x - rand) < 2)
                {
                    c = new Color(1, 1, 1);
                }
                else
                {
                    c = new Color(0, 0, 0, 0);
                }
                lightning.SetPixel(x, y, c);
            }
        }

        lightning.Apply();
        return lightning;
    }
}
