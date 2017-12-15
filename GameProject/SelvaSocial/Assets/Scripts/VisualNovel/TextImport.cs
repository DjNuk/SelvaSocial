using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextImport : MonoBehaviour
{
    public GameObject textBox;

    public Text theText;

    [SerializeField]
    TextAsset textFile;

    public string[] textLines;

    public int currentLine;
    public int endLine;

    void Start()
    {
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if (endLine == 0)
            endLine = textLines.Length - 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentLine <= endLine)
            {
                StartCoroutine(EfeitoDigitacao(textLines[currentLine]));
                currentLine += 1;
            }
            else
            {
                textBox.SetActive(false);
            }
        }
    }

    IEnumerator EfeitoDigitacao(string sentence)
    {
        theText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            theText.text += letter;
            yield return null;


            if (Input.GetKeyDown(KeyCode.Z))
            {
                theText.text = sentence;
                break;
            }
        }
    }
}
