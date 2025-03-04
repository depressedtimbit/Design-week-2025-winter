using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneManager : MonoBehaviour
{
    public GameObject screenA, screenB;

    private bool atScreenA = true;

    // set to true while the main scene is active, i.e. when no puzzles / overlays are active
    // when false, ignores keypresses / clicks that would change the view
    private bool inMainScreen = true;

    // represents the root object of the currently loaded additive scene
    // when a scene is loaded in additively, all its root objects are parented to this object, so that they can be deleted instantly when we want to unload the scene
    // (actually dont worry about this yet, maybe scenemanager.unloadsceneasync will work - my concern is that it'll cause a bit of lag in the final build, destroying might be faster)
    //public GameObject additiveSceneRoot;

    // object that is set inactive when an additive scene is loaded, then active when the scene is de-loaded
    // for now, just the camera and eventsystem are parented to it, but later we could just parent the whole base scene to it
    public GameObject deactivateOnSceneLoad;

    // name of the test scene that will be loaded additively 
    public string additiveSceneToLoad;

    private Scene loadedScene;

    // Start is called before the first frame update
    void Start()
    {
        SetScreens(atScreenA);
    }

    // Update is called once per frame
    void Update()
    {
        if (inMainScreen)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetScreens(!atScreenA);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (inMainScreen)
            {
                // additively load the test scene
                var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
                loadedScene = SceneManager.LoadScene(additiveSceneToLoad, parameters);

                // https://discussions.unity.com/t/find-gameobject-in-another-loaded-scene/245359/2
                // use scene.getrootgameobjects if we need em later

            }
            else
            {
                // unload the test scene 
                SceneManager.UnloadSceneAsync(loadedScene);
            }

            inMainScreen = !inMainScreen;
            deactivateOnSceneLoad.SetActive(inMainScreen);
        }
    }

    private void SetScreens(bool atScreenA)
    {
        this.atScreenA = atScreenA;

        screenA.SetActive(atScreenA);
        screenB.SetActive(!atScreenA);
    }
}
