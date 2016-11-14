using UnityEngine;
using System.Collections;

public class TextureChanger : MonoBehaviour
{
    public Texture[] textures;
    public int currentTexture;
    public Renderer renderer;
    const float delay = 0.05f;
    public GameObject[] particleHolders;
    private Vector3 moveBy;
    private float startX;

    // Use this for initialization
    void Start()
    {
        startX = 0;
        renderer = GetComponent<Renderer>();
        foreach (GameObject child in particleHolders)
        {
            ParticleSystem emitter = child.GetComponent<ParticleSystem>();
            emitter.Stop();
        }
        moveBy = new Vector3(-1.3f, 0, 0);
        renderer.material.mainTexture = textures[0];
    }


    private IEnumerator ChangeTextures()
    {
        foreach (GameObject child in particleHolders)
        {
            ParticleSystem emitter = child.GetComponent<ParticleSystem>();
            // reset the position
            child.transform.position = new Vector3(startX, child.transform.position.y, child.transform.position.z);
            emitter.Play();
        }
        while (currentTexture < textures.Length)
        {
            renderer.material.mainTexture = textures[currentTexture];
            currentTexture++;
            //move the particles
            foreach (GameObject child in particleHolders)
            {
                moveBy = -moveBy;
                StartCoroutine(child.gameObject.GetComponent<particleMover>().Translation(child.gameObject.transform, moveBy, delay));
            }


            yield return new WaitForSeconds(delay);
        }
        foreach (GameObject child in particleHolders)
        {
            ParticleSystem emitter = child.GetComponent<ParticleSystem>();
            emitter.Stop(false);
        }
        currentTexture = 0;
    }

    public void StartUpgrade()
    {
        StartCoroutine(ChangeTextures());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartUpgrade();
        }
    }
}
