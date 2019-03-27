using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class UtiCursedController : MonoBehaviour
{
    public AudioClip[] soundArray;
    GameObject[] soundClips = new GameObject[15];
    int currentArrayValue = 0;
    int objsInstantiated = 0;
    public int cursed = 0;

    ContrastEnhance contrast;
    Tonemapping tonemap;
    Grayscale gray;
    MotionBlur blur;
    VignetteAndChromaticAberration vignette;

    public Shader[] shaders;
    public Texture2D texture2D;
    public AudioClip drums;

    public GameObject ghost;

    bool otherScene = true;

    // Start is called before the first frame update
    void Start()
    {
        GameObject hello = Instantiate(new GameObject("drums"));
        hello.AddComponent<AudioSource>().clip = drums;
        DontDestroyOnLoad(hello);
        hello.GetComponent<AudioSource>().loop = true;
        hello.GetComponent<AudioSource>().Play();
        hello.AddComponent<DestroyOnDeath>();
        hello.AddComponent<UtiDestroyWhenNotCursed>();
        this.gameObject.AddComponent<DestroyOnDeath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            GameObject.Find("UtiCursed(Clone)").GetComponent<UtiCursedController>().ReduceCursed(1);
            if (GameObject.Find("UtiWolfFamiliar(Clone)"))
            {
                Destroy(GameObject.Find("UtiWolfFamiliar(Clone)").gameObject);
            } else
            {
                GameObject.Find("UtiWolfHead(Clone)").GetComponent<Tile>().dropped(GameObject.Find("player_tile(Clone)").GetComponent<Tile>());
                Destroy(GameObject.Find("UtiWolfHead(Clone)").gameObject);
            }
            
        }

        if (SceneManager.GetActiveScene().name =="PlayScene")
        {
            if (otherScene)
            {
                GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

                foreach (GameObject go in allObjects)
                {
                    if (go.GetComponent<Tile>())
                    {
                        if(go.GetComponent<Tile>().hasTag(TileTags.Creature) ||
                        go.GetComponent<Tile>().hasTag(TileTags.Enemy) ||
                        go.GetComponent<Tile>().hasTag(TileTags.Friendly)) {

                            go.AddComponent<UtiSpawnGhost>().ghost = ghost;
                        }
                    }
                }

                otherScene = false;
            }
            /*
            if (Camera.main.gameObject.GetComponent<Bloom>() == false)
            {
                Camera.main.gameObject.AddComponent<Bloom>();
            }*/
            
            if (Camera.main.gameObject.GetComponent<ContrastEnhance>() == false)
            {
                contrast = Camera.main.gameObject.AddComponent<ContrastEnhance>();
                contrast.separableBlurShader = shaders[0];
                contrast.contrastCompositeShader = shaders[1];
            }
            if (Camera.main.gameObject.GetComponent<Tonemapping>() == false)
            {
                tonemap = Camera.main.gameObject.AddComponent<Tonemapping>();
                tonemap.tonemapper = shaders[2];
            }
            if (Camera.main.gameObject.GetComponent<Grayscale>() == false)
            {
                gray = Camera.main.gameObject.AddComponent<Grayscale>();
                gray.shader = shaders[3];
            }
            if (Camera.main.gameObject.GetComponent<VignetteAndChromaticAberration>() == false)
            {
                vignette = Camera.main.gameObject.AddComponent<VignetteAndChromaticAberration>();
                vignette.vignetteShader = shaders[7];
                vignette.separableBlurShader = shaders[8];
                vignette.chromAberrationShader = shaders[9];
            }
        } else
        {
            otherScene = true;
        }

        
        contrast.intensity = cursed * cursed * 3f;

        tonemap.exposureAdjustment = cursed * 1.5f + 1.5f;

        vignette.intensity = cursed / 20f + 0.35f;
        vignette.intensity = Mathf.Clamp(vignette.intensity, 0f, 0.8f);

        vignette.chromaticAberration = cursed * 10f + Random.Range((cursed * 4f)/-7, (cursed * 4f) / 7);

        vignette.blur = cursed / 12 + 0.5f;
        vignette.blur = Mathf.Clamp(vignette.intensity, 0f, 0.4f);
        vignette.blurSpread = 100f;

       
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            GameObject hallo = Instantiate(new GameObject("note1"), this.transform);
            hallo.AddComponent<AudioSource>().clip = soundArray[Random.Range(0, soundArray.Length)];
            hallo.GetComponent<AudioSource>().Play();
            hallo.GetComponent<AudioSource>().loop = true;
            hallo.AddComponent<UtiDestroyWhenNotCursed>();
            soundClips[currentArrayValue] = hallo;
            

            currentArrayValue++;
            objsInstantiated++;
            if (currentArrayValue>soundClips.Length-1)
            {
                currentArrayValue = 0;
            }
            if (objsInstantiated > soundClips.Length - 1)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
        }

        if (cursed == 0)
        {
            if (Camera.main.gameObject.GetComponent<ContrastEnhance>())
            {
                Destroy(Camera.main.gameObject.GetComponent<ContrastEnhance>());
            }
            if (Camera.main.gameObject.GetComponent<Tonemapping>())
            {
                Destroy(Camera.main.gameObject.GetComponent<Tonemapping>());
            }
            if (Camera.main.gameObject.GetComponent<Grayscale>())
            {
                Destroy(Camera.main.gameObject.GetComponent<Grayscale>());
            }
            if (Camera.main.gameObject.GetComponent<VignetteAndChromaticAberration>());
            {
                Destroy(Camera.main.gameObject.GetComponent<VignetteAndChromaticAberration>());
            }
            Destroy(this.gameObject);
        }
    }


    public void ReduceCursed(int x)
    {
        cursed -= x;
    }

    public void AddCursed(int x)
    {
        cursed += x;
    }
}
