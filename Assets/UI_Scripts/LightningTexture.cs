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

    float Noise(float y)
    {
        return Mathf.PerlinNoise(y / ((float)textureY / 10.0f), Random.value * 100f)
            + (Mathf.PerlinNoise(y / ((float)textureY / 5.0f), Random.value * 100f) / 2.0f);
    }

    Texture MakeLightningTexture()
    {
        float last = Random.value * textureX;
        Texture2D lightning = new Texture2D(textureX, textureY);
        for (int s = 0; s < textureY; s += 10)
        {
            float rand = Random.value * textureX;
            for (int y = 0; y < 10 && (s + y < textureY); y++)
            {
                for (int x = 0; x < textureX; x++)
                {
                    float posX = Mathf.Lerp(last, rand, y / 10f);
                    Color c;
                    if (Mathf.Abs(x - posX) < 2)
                    {
                        c = new Color(1, 1, 1);
                    }
                    else
                    {
                        c = new Color(0, 0, 0, 0);
                    }
                    lightning.SetPixel(x, s + y, c);
                }
            }
            last = rand;
        }

        lightning.Apply();
        return lightning;
    }
}
