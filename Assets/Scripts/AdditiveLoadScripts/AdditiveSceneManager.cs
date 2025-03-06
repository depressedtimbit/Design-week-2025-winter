using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneManager : MonoBehaviour
{
    public static AdditiveSceneManager Instance { get; private set;}
    
    // set to true while the main scene is active, i.e. when no puzzles / overlays are active
    // when false, ignores keypresses / clicks that would change the view
    private bool inMainScreen = true;


    // represents the root object of the currently loaded additive scene
    // when a scene is loaded in additively, all its root objects are parented to this object, so that they can be deleted instantly when we want to unload the scene
    // (actually dont worry about this yet, maybe scenemanager.unloadsceneasync will work - my concern is that it'll cause a bit of lag in the final build, destroying might be faster)
    //public GameObject additiveSceneRoot;

    // object that is set inactive when an additive scene is loaded, then active when the scene is de-loaded
    // for now, just the camera and eventsystem are parented to it, but later we could just parent the whole base scene to it
    //public GameObject deactivateOnSceneLoad;

    private Scene loadedScene;
    
    private GameObject deactivateOnSceneLoad;
    private string additiveSceneToLoad;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
        {
            Debug.Log("Multiple SceneManager's exist, deleting the latest one");
            Destroy(this);
        }
    }


    // Update is called once per frame
    public void LoadScene(string additiveSceneToLoad, GameObject deactivateOnSceneLoad)
    {
        if (inMainScreen)
            {
                inMainScreen = false;
                this.additiveSceneToLoad = additiveSceneToLoad;
                this.deactivateOnSceneLoad = deactivateOnSceneLoad;
                CurtainManager.instance.FadeIn(1).setOnComplete(() =>
                {
                    // additively load the scene
                    var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
                    loadedScene = SceneManager.LoadScene(additiveSceneToLoad, parameters);

                    // https://discussions.unity.com/t/find-gameobject-in-another-loaded-scene/245359/2
                    // use scene.getrootgameobjects if we need em later

                    CurtainManager.instance.FadeOut(1);

                    deactivateOnSceneLoad.SetActive(inMainScreen);
                });

            }
        else Debug.LogError("Tried to Load a puzzle whilst loaded in a puzzle, this is bad!");
    }

    public void unloadScene(int unlockedToolID)
    {
        if (!inMainScreen)
        {
            inMainScreen = true;
            CurtainManager.instance.FadeIn(1).setOnComplete(() =>
            {
                // unload the test scene 
                SceneManager.UnloadSceneAsync(loadedScene);

                deactivateOnSceneLoad.SetActive(inMainScreen);

                //set player as having won the puzzle
                PlayerData.Instance.ToolStates[unlockedToolID] = true;

                CurtainManager.instance.FadeOut(1).setOnComplete(() => 
                {
                    switch (unlockedToolID)
                    {
                        case 1:
                            SubtitleManager.instance.DoDialogue("I got the saw tool!");
                            break;
                        case 2:
                            SubtitleManager.instance.DoDialogue("I got the welding torch!");
                            break;
                        case 3:
                            SubtitleManager.instance.DoDialogue("I got the electrical probe!");
                        break;


                        default: Debug.Log("invalid Tool ID"); 
                        break;
                    }
                });
                

                
            });
        }
        else Debug.LogError("Tried to unload a puzzle without one being loaded, this is bad!");
    }

    public void restartScene()
    {
        if (!inMainScreen)
        {
            // unload scene
            SceneManager.UnloadSceneAsync(loadedScene);

            // additively load the scene
            var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
            loadedScene = SceneManager.LoadScene(additiveSceneToLoad, parameters);
        }
    }
}
