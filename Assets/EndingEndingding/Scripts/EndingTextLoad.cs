using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

// This Script is for managing Ending texts
//
//      TextMeshProUGUI dialogueText : gets UI text from inspector
// List<DialogueGroup> dialogueLines : gets dialogues from inspector for each endings
//                        int ending : saves ending by EndingLoad
//                                     This script provides setter and getter for the ending value
public class EndingTextLoad : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;                // UI Text (또는 TextMeshProUGUI)
    public List<DialogueGroup> dialogueLines;         // 대사 리스트
    public float typingSpeed = 0.05f;          
    public int ending;

    private int currentLine = 0;
    private bool isTyping = false;

    void Start()
    {
        StartCoroutine(TypeLine(dialogueLines[ending].lines[currentLine]));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            currentLine++;

            if (currentLine < dialogueLines[ending].lines.Count)
            {
                StartCoroutine(TypeLine(dialogueLines[ending].lines[currentLine]));
            }
            else
            {
                SceneManager.LoadScene("EndingListsScene");
            }
        }
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
    public void setEnding(int i)
    {
        ending = i;
    }
    public int getEnding()
    {
        return ending;
    }
}
