using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mat_Off_Anim : MonoBehaviour {
    public float dx = 0;
    public float dy = 1;
    Material mat;
    // Use this for initialization
	void Start () {
        mat = GetComponent<MeshRenderer>().material;
        
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 offset = mat.GetTextureOffset("_MainTex");
        if (Mathf.Abs(offset.x) >= (dx * 10)) offset.x = 0;
        if (Mathf.Abs(offset.y) >= (dy * 10)) offset.y = 0;
        mat.SetTextureOffset("_MainTex", new Vector2(offset.x + (dx * Time.deltaTime), offset.y + (dy * Time.deltaTime)));
	}
}
