using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerText : MonoBehaviour
{
    public RectTransform bubble;

    public float horizontalOffset = 1.8f;
    public float verticalOffset = 1.8f;

    public List<string> dialogues;
    int textIterator;

    float showTime;
    bool showingText;

    //Image img;
    Text txt;

    Color modifier;

    private void Start()
    {
        //img = bubble.GetChild(0).GetComponent<Image>();
        txt = bubble.GetChild(1).GetComponent<Text>();
        bubble.gameObject.SetActive(false);
        textIterator = 0;
        showingText = false;
        modifier = new Color(1, 1, 1, 0);
    }

    private void Update()
    {
        bubble.position = new Vector3(transform.position.x + horizontalOffset, transform.position.y + verticalOffset, bubble.position.z);

        if (textIterator > dialogues.Count - 1)
            textIterator = 0;

        if (showingText && txt.color.a < 1)
        {
            modifier = new Color(1, 1, 1, modifier.a + 0.075f);
            //img.color = modifier;
            txt.color = modifier;
        }

        if (!showingText && txt.color.a >= 1)
        {
            modifier = new Color(1, 1, 1, modifier.a - 0.1f);
            //img.color = modifier;
            txt.color = modifier;
        }

        if (Time.time - showTime >= 5)
            showingText = false;
        if (Time.time - showTime >= 6)
            HideText();

    }

    public void ShowText()
    {
        string text = dialogues[textIterator++];
        text = text.Replace("\\n", "\n");
        txt.text = text;
        bubble.gameObject.SetActive(true);
        showingText = true;

        showTime = Time.time;
    }

    public void HideText()
    {
        bubble.gameObject.SetActive(false);
    }
}