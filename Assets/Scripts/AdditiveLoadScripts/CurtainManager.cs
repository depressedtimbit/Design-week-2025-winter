using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The class that manages the screen curtain (i.e. fade to black effect)
/// You can remotely call a fade to / from black with CurtainManager.instance.FadeIn and FadeOut
/// The functions return a LTDescr object to which you can chain a .SetOnComplete() lambda in order to
/// make something happen when the fade is complete.
/// You can also set ease functions on it.
/// </summary>
public class CurtainManager : MonoBehaviour
{
    #region Singleton
    public static CurtainManager instance;

    private void Awake()
    {
        // set instance in Awake() so other classes can reference it in Start()
        instance = this;
    }
    #endregion

    // reference to the curtain image
    public Image curtain;


    public LTDescr FadeIn(float time)
    {
        return Fade(true, time);
    }

    public LTDescr FadeOut(float time)
    {
        return Fade(false, time);

    }

    public LTDescr Fade(bool on, float time)
    {
        // set from and to values for the curtain alpha
        float from = on ? 0 : 1;
        float to = on ? 1 : 0;

        // cancel ongoing tweens on the curtain
        LeanTween.cancel(gameObject);

        return LeanTween.value(gameObject, from, to, time).setOnUpdate((float val) =>
        {
            Color c = curtain.color;
            c.a = val;
            curtain.color = c;
        });
    }

}
