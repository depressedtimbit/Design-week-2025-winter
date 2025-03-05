using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set;}

    //always True, Saw, Welder, Probe
    public bool[] ToolStates = new bool[4]{true, false, false, false};

    public int PlayerView = 0;
    public bool[] DoorStates = new bool[4]{false, false, false, false};

    // Awake is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
        {
            Debug.Log("Multiple PlayerData's exist, deleting the latest one");
            Destroy(this);
        }
    }

    
}
