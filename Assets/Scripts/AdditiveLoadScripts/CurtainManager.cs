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
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

        ditherMaterial = ditherImage.material;
    }
    #endregion

    // reference to the curtain image
    public Image curtain;
    public Image explosionCurtain;

    private Material ditherMaterial;
    public Image ditherImage;

    public LTDescr FadeIn(float time, bool regularCurtain = true)
    {
        return Fade(true, time, regularCurtain);
    }

    public LTDescr FadeOut(float time, bool regularCurtain = true)
    {
        return Fade(false, time, regularCurtain);

    }

    // if regularCurtain is set to false, the white explosion curtain will fade in instead
    public LTDescr Fade(bool on, float time, bool regularCurtain = true)
    {
        // set from and to values for the curtain alpha
        float from = on ? 0 : 1;
        float to = on ? 1 : 0;



        Image curtainImage = regularCurtain ? curtain : explosionCurtain;

        // cancel ongoing tweens on the curtain
        LeanTween.cancel(curtainImage.gameObject);

        return LeanTween.value(curtainImage.gameObject, from, to, time).setOnUpdate((float val) =>
        {
            Color c = curtainImage.color;
            c.a = val;
            curtainImage.color = c;
        });
    }

    /// <summary>
    /// Referenced https://discussions.unity.com/t/create-texture-from-current-camera-view/86847/2
    /// </summary>
    /// <param name="oldCam"></param>
    /// <param name="newCam"></param>
    /// <returns></returns>
    public LTDescr DitherIn(View oldView, View newView, float time)
    {
        Camera oldCam = oldView.cam, newCam = newView.cam;

        Rect rect = new Rect(0, 0, Screen.width, Screen.height);

        Texture2D oldTex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);
        Texture2D newTex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);

        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);

        oldCam.targetTexture = renderTexture;
        oldCam.Render();

        RenderTexture.active = renderTexture;
        oldTex.ReadPixels(rect, 0, 0);
        oldTex.Apply();

        oldCam.targetTexture = null;
        RenderTexture.active = null;

        // repeat for new camera

        newView.gameObject.SetActive(true);

        newCam.targetTexture = renderTexture;
        newCam.Render();

        RenderTexture.active = renderTexture;
        newTex.ReadPixels(rect, 0, 0);
        newTex.Apply();

        newCam.targetTexture = null;
        RenderTexture.active = null;

        // set textures on material
        ditherMaterial.SetTexture("_FromTexture", oldTex);
        ditherMaterial.SetTexture("_ToTexture", newTex);

        newView.gameObject.SetActive(false);

        LeanTween.cancel(ditherImage.gameObject);

        Color c = ditherImage.color;
        c.a = 1;
        ditherImage.color = c;

        LeanTween.value(0, 1, time).setOnComplete(() =>
        {
            Color c2 = ditherImage.color;
            c2.a = 0;
            ditherImage.color = c2;

            Destroy(oldTex);
            Destroy(newTex);
        });

        return LeanTween.value(ditherImage.gameObject, 0, 1, time).setOnUpdate((float val) =>
        {
            ditherMaterial.SetFloat("_Dither", val);
        });
    }

}
