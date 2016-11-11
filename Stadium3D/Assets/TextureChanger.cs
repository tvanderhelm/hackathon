using UnityEngine;
using System.Collections;

public class TextureChanger : MonoBehaviour {
    public Texture[] textures;
    public int currentTexture;
    public Renderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space)) {
            currentTexture++;
            currentTexture %= textures.Length;
            renderer.material.mainTexture = textures[currentTexture];   
        }
	}
}
