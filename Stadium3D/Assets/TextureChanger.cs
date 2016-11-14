﻿using UnityEngine;
using System.Collections;

public class TextureChanger : MonoBehaviour {
    public Texture[] textures;
    public int currentTexture;
    public Renderer renderer;
    const float delay = 0.05f;
    public GameObject child1;
    public GameObject child2;
    particleMover child1Script;
    private Vector3 moveBy;

    // Use this for initialization
    void Start () {
        renderer = GetComponent<Renderer>();
        child1Script = child1.gameObject.GetComponent<particleMover>();
        child1.active = false;
        child2.active = false;
        moveBy = new Vector3(-1.5f, 0, 0);
    }

    private IEnumerator ChangeTextures()
    {
        child1.active = true;
        child2.active = true;
        while (currentTexture < textures.Length) {
            renderer.material.mainTexture = textures[currentTexture];
            currentTexture++;
            //move the particles
            StartCoroutine(child1Script.Translation(child1.gameObject.transform, moveBy, delay));

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
