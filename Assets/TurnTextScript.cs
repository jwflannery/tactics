using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnTextScript : MonoBehaviour {

    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        text.text = "";
	}

    public void DisplayText(string currentTeamName)
    {
        text.text = currentTeamName + " Turn";
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        StartCoroutine(FadeTextToFullAlpha(1f, text));
    }
	
    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
        StartCoroutine(FadeTextToZeroAlpha(1f, text));
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        text.text = "";
    }
}
