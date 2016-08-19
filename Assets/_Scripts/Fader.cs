using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Fader : MonoBehaviour
{

    public static Fader instance;
    public float fadeSpeed = 1.5f;


    public bool fadeClear = true;
    public bool fadeBlack = false;

    
    public Image img;

    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        img.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
    }

    public void SetClear()
    {
        print("<color=purple>Setting color clear.</color>");
        fadeBlack = false;
        fadeClear = true;
    }

    public void SetBlack()
    {
        print("<color=purple>Setting color black.</color>");
        fadeClear = false;
        img.color = new Color(0, 0, 0, 1);
        img.enabled = true;
    }

    void Update()
    {
        if (fadeClear)
            FadeToClear();

        if (fadeBlack)
            FadeToBlack();
    }

    public void FadeToClear()
    {
        if (!img.enabled)
            img.enabled = true;



        img.color = Color.Lerp(img.color, Color.clear, fadeSpeed * Time.deltaTime);

        if (img.color.a <= 0.05f)
        {
            // ... set the colour to clear and disable the guiT.
            img.color = Color.clear;
            img.enabled = false;

            print("<color=purple>Alpha is under .05, finished clearing</color>");
            fadeClear = false;
        }
    }


    public void FadeToBlack()
    {
        if(!img.enabled)
            img.enabled = true;

        img.color = Color.Lerp(img.color, Color.black, fadeSpeed * Time.deltaTime);
       

        if (img.color.a >= 0.95f)
        {
            // ... set the colour to clear and disable the guiT.
            img.color = Color.black;

            print("<color=purple>Alpha is over .95, finished darkening</color>");
            fadeBlack = false;
        }
    }

}
