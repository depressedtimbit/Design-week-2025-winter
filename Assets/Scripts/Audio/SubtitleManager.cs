using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public GameObject subtitlePrefab;

    [Header("Dialogue Variables")]
    public TextMeshProUGUI dialogueText;
    public Image dialogueBG;
    public float dialogueDisplayTimePerLetter = 0.1f;
    public float dialogueBGFadeInTime = 0.75f;

    public float dialogueLifetime = 7, dialogueFadeTime = 2f;
    public bool loadInFromCenter = false;

    // the alpha value that the background is at when it is at maximum alpha
    public float dialogueBGFullAlpha = 0.7f;

    public void DoSubtitle(string subText)
    {
        if (subText.Length == 0) return;

        GameObject newSub = Instantiate(subtitlePrefab,transform);

        // set the instance's parent to our transform and set it as the first (top) sibling
        newSub.transform.SetAsFirstSibling();

        newSub.GetComponent<SubtitleObject>().InitSubtitle(subText);
        
    }

    public void DoDialogue(string dialogue)
    {
        LeanTween.cancel(dialogueText.gameObject);

        dialogueText.text = "";
        SetTextAlpha(dialogueText, 1);


        float displayTime = dialogue.Length * dialogueDisplayTimePerLetter;

        float benchmark = dialogueDisplayTimePerLetter;
        int displayingLetters = 0;

        // fade IN black background
        LeanTween.value(dialogueText.gameObject, 0, 1, dialogueBGFadeInTime).setOnUpdate((float val) =>
        {
            Color c = dialogueBG.color;
            c.a = val;
            dialogueBG.color = c;
        });

        LeanTween.value(dialogueText.gameObject, 0, displayTime, displayTime).setOnUpdate((float val) =>
        {
            if (val > benchmark)
            {
                displayingLetters++;
                benchmark += dialogueDisplayTimePerLetter;

                dialogueText.text = dialogue.Substring(0, displayingLetters) + (loadInFromCenter ? "" : new string(Enumerable.Repeat(' ', dialogue.Length - displayingLetters).ToArray()));
            }
        });

        LeanTween.value(dialogueText.gameObject, 0, 1, dialogueLifetime).setOnComplete(() =>
        {
            LeanTween.value(dialogueText.gameObject, 1, 0, dialogueFadeTime).setOnUpdate((float val) =>
            {
                SetTextAlpha(dialogueText, val);

                Color c = dialogueBG.color;
                c.a = val * dialogueBGFullAlpha;
                dialogueBG.color = c;
            });

        });
    }

    private void SetTextAlpha(TextMeshProUGUI text, float alpha)
    {
        Color c = text.color;
        c.a = alpha;
        text.color = c;
    }
}
