using UnityEngine;
using FMOD;
using FMODUnity;
public static class FMODController 
{
    private static FMOD.Studio.EventInstance eventInstance;


    public static void Start()
    {
        if (eventInstance.isValid())
        {
            eventInstance.setParameterByName("Music State", 0);
        }
        eventInstance = RuntimeManager.CreateInstance("event:/Music/Music");
        eventInstance.start();

    }

    public static void UpdateState(int val)
    {
        eventInstance.setParameterByName("Music State", 1);
        float yes = -1f;
        eventInstance.getParameterByName("Music State", out yes);
        UnityEngine.Debug.Log(yes);
    }
}
