using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueHandler : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    public CanvasRenderer textPanel;
    public Queue<string> lineQueue = new Queue<string>();

    private void Awake()
    {
        textPanel = GetComponent<CanvasRenderer>();
        HidePanel();
    }

    private void ShowPanel()
    {
        textPanel.SetAlpha(128);
    }

    public void HidePanel()
    {
        text.text = "";
        textPanel.SetAlpha(0);
    }

    public void ReadLines(string[] lineArray)
    {
        lineQueue.Clear();
        foreach (string line in lineArray)
        {
            lineQueue.Enqueue(line);
        }
        ShowPanel();
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeSentence(lineQueue.Dequeue()));
    }

    IEnumerator TypeSentence(string sentence)
    {
        text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            yield return null;
        }
    }
}
