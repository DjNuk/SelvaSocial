using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VisualNovelManager : MonoBehaviour
{
    public Sprite[] Images;

    public Image ImageUI;
    public Text theText;

    [SerializeField]
    TextAsset text;

    public string[] textLines;

    public int currentLine;
    public int endLine;

    bool end = true;

    void Start()
    {
        StartCoroutine(EfeitoDigitacao(textLines[currentLine]));
        ImageUI.sprite = Images[currentLine];
        currentLine += 1;

        if (endLine == 0)
            endLine = textLines.Length - 1;
    }

    void Update()
    {
        if (Input.GetButtonDown("XboxA") && end)
        {
            if (currentLine <= endLine)
            {
                end = false;
                StartCoroutine(EfeitoDigitacao(textLines[currentLine]));
                ImageUI.sprite = Images[currentLine];
                currentLine += 1;
            }
            else
            {
                SceneManager.LoadScene(2);
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

        end = true;
    }
}
