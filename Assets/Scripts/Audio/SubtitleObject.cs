using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitleObject : MonoBehaviour
{
    public float lifeTime = 3.5f, fadeTime = 0.5f;

    private TextMeshProUGUI text;

    public float bounceAmount = 50f;
    public float offScreenAmount = 150f;
    public float bounceTime = 0.3f;

    public void InitSubtitle(string subText)
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = $"({subText.ToUpper()})";

        // bounce text
        RectTransform rt = text.rectTransform;
        Vector3 startPos = rt.localPosition;
        LeanTween.value(startPos.x - offScreenAmount, startPos.x + bounceAmount, bounceTime / 2f).setOnUpdate((float val) =>
        {
            Vector3 pos = rt.localPosition;
            pos.x = val;
            rt.localPosition = pos;
        }).setOnComplete(() =>
        {
            LeanTween.value(startPos.x + bounceAmount, startPos.x, bounceTime / 2f).setOnUpdate((float val) =>
            {
                Vector3 pos = rt.localPosition;
                pos.x = val;
                rt.localPosition = pos;
            });
        });

        // do lifetime / fade
        LeanTween.value(0, 1, lifeTime).setOnComplete(() =>
        {
            LeanTween.value(1, 0, fadeTime).setOnUpdate((float val) =>
            {
                Color c = text.color;
                c.a = val;
                text.color = c;

            }).setOnComplete(() =>
            {
                Destroy(gameObject);
            });
        });
    }

}
