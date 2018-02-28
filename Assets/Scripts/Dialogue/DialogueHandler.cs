using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueHandler : MonoBehaviour
{
    private TextMeshProUGUI text;
    private CanvasRenderer textPanel;
    private Image portraitImage;
    public Queue<DialogueLines.Line> lineQueue = new Queue<DialogueLines.Line>();

    private void Start()
    {
        textPanel = GetComponent<CanvasRenderer>();
        text = ObjectReferences.Instance.DialogueText;
        portraitImage = ObjectReferences.Instance.PortraitImage;
        HidePanel();
    }

    private void ShowPanel()
    {
        textPanel.SetAlpha(128);
        portraitImage.canvasRenderer.SetAlpha(128);
    }

    public void HidePanel()
    {
        StopAllCoroutines();
        text.text = "";
        textPanel.SetAlpha(0);
        portraitImage.canvasRenderer.SetAlpha(0);
    }

    public void ReadLines(DialogueLines.Line[] lineArray)
    {
        lineQueue.Clear();
        foreach (DialogueLines.Line line in lineArray)
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

    IEnumerator TypeSentence(DialogueLines.Line line)
    {
        text.text = "";
        if (line.PortaitImage != null)
            portraitImage.sprite = line.PortaitImage;
        foreach (char letter in line.LineString.ToCharArray())
        {
            text.text += letter;
            yield return null;
        }
    }
}
