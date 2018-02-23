using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseTexture : MonoBehaviour {

    private Texture texture;
    private RawImage image;
    public int textureSize = 64;

    private float last = Time.time;

	// Use this for initialization
	void Start () {
        image = GetComponent<RawImage>();
        texture = MakeNoiseTexture();
        image.texture = texture;
    }
	
	// Update is called once per frame
	void Update () {
        texture = MakeNoiseTexture();
        image.texture = texture;
	}

    Texture MakeNoiseTexture()
    {
        Texture2D noise = new Texture2D(textureSize, textureSize);
        float rand = Random.value;
        for (int x = 0; x < textureSize; x++)
        {
            for (int y = 0; y < textureSize; y++)
            {
                Color c = new Color(1, 1, 1)
                    * (0.9f + (0.1f * Mathf.PerlinNoise(
                        (x * 50f + rand * 5000f) / (float) textureSize,
                        (y * 100f + rand * 200f) / (float) textureSize / 50f
                        )));
                noise.SetPixel(x, y, c);
            }
        }

        noise.Apply();
        return noise;
    }
}
