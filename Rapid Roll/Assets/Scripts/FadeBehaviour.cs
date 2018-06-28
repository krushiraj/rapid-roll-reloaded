using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeBehaviour : MonoBehaviour
{

    public float fadeSpeed = 1.5f,fadeTime = 1f;

    private bool sceneStarting = true;
    private GUITexture fader;
    void Awake()
    {
        fader = GameObject.Find("Fader").GetComponent<GUITexture>();
        fader.pixelInset = new Rect(0,0,Screen.width,Screen.height);
        StartCoroutine( FadeToClear());
        //FadeToClear();
    }

    void Update()
    {
        if (sceneStarting)
        {
            StartScene();
        }
    }

    void StartScene()
    {
        FadeToClear();
        if (fader.color.a <= 0.05f)
        {
            fader.color = Color.clear;
            fader.enabled = false;

            sceneStarting = false;
        }
    }

    public IEnumerator FadeToBlack()
    {
        fader.gameObject.SetActive(true);
        fader.color = Color.clear;

        float rate = 1 / fadeTime;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            fader.color = Color.Lerp(Color.clear, Color.black, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }

        fader.color = Color.black;
        fader.gameObject.SetActive(true);
    }

    public IEnumerator FadeToClear()
    {
        fader.gameObject.SetActive(true);
        fader.color = Color.black;

        float rate = 1 / fadeTime;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            fader.color = Color.Lerp(Color.black, Color.clear, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }

        fader.color = Color.clear;
        fader.gameObject.SetActive(true);
    }

    public void EndSceneAndLoadIndex(int index)
    {
        fader.enabled = true;
        FadeToBlack();
        if (fader.color.a >= 0.95f)
        {
            SceneManager.LoadScene(index);
        }
    }

}