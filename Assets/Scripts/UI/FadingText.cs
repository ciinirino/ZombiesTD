using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*SUMMARY (Updated oct 26 2022)
 * 
 * This script decreases the alpha of a text object over time
 * Also has a function to reset the alpha to full
 * If this script is attached to a text, it will always fade.
 *      !add componenet to turn fading on/off
 */
public class FadingText : MonoBehaviour
{
    //determines if this should start with alpha set to zero
    public bool startFaded;

    //time untill the fading effect begins
    public float fadeDelay;
    float timeTillFade;

    public float fadeSpeed;


    TMP_Text text;
    Color textColor;

    // Start is called before the first frame update
    void Start()
    {
        timeTillFade = fadeDelay;
        text = GetComponent<TMP_Text>();
        textColor = text.color;
        if(startFaded)
        {
            textColor.a = 0;
            text.color = textColor;
        }

    }
    void Update()
    {
        Fade();
    }
    public void Fade()
    {
        if (timeTillFade > 0)
        {
            timeTillFade -= Time.deltaTime;
        }
        else
        {
            if (textColor.a > 0)
            {
                textColor.a -= Time.deltaTime / fadeSpeed;
                text.color = textColor;
            }
        }
    }
    public void ResetFade()
    {
        timeTillFade = fadeDelay;
        textColor.a = 1;
        text.color = textColor;
    }
}
