﻿using UnityEngine;
using System.Collections;

public class TextureChanger : MonoBehaviour {
    public Texture[] textures;
    public int currentTexture;
    public Renderer renderer;
    const float delay = 0.05f;
    public GameObject[] particleHolders;
    private Vector3 moveBy;

    // Use this for initialization
    void Start () {
        renderer = GetComponent<Renderer>();
        foreach (GameObject child in particleHolders)
        {
            child.active = false;
        }
        moveBy = new Vector3(-1.5f, 0, 0);
    }

    private IEnumerator ChangeTextures()
    {
        foreach (GameObject child in particleHolders)
        {
            child.active = true;
        }
        while (currentTexture < textures.Length) {
            renderer.material.mainTexture = textures[currentTexture];
            currentTexture++;
            //move the particles
            foreach (GameObject child in particleHolders)
            {
                StartCoroutine(child.gameObject.GetComponent<particleMover>().Translation(child.gameObject.transform, moveBy, delay));
            }
            

            yield return new WaitForSeconds(delay);
        }
        currentTexture = 0;
    }

    public void StartUpgrade() {
        StartCoroutine(ChangeTextures());
        //child1Script.moving = true;
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space)) {
            StartUpgrade();
        }
	}
}
