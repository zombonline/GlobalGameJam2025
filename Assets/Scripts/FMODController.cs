using UnityEngine;
using FMOD;
using FMODUnity;
using Unity.VisualScripting;
using FMOD.Studio;
public static class FMODController 
{
    private static FMOD.Studio.EventInstance musicEvent;
    public static void InitiateMusic()
    {
        if (musicEvent.isValid()) { return; }
        musicEvent = RuntimeManager.CreateInstance("event:/Music/Music");
        musicEvent.start();
    }
    public static void UpdateMusicState(int val)
    {
        musicEvent.setParameterByName("Music State", 1);
    }

    /// <summary>
    /// Creates a new event instance and plays it.
    /// </summary>
    /// <param name="path">The path to locate the event in the FMOD project.</param>
    /// <returns>Returns the unique instance of the sfx event.</returns>
    public static EventInstance PlaySFX(string path)
    {
        EventInstance sfx = RuntimeManager.CreateInstance(path);
        sfx.start();
        return sfx;
    }
    /// <summary>
    /// Creates a new event instance and plays it.
    /// </summary>
    /// <param name="eventReference">The fmod event reference of the SFX, allows the parameter to be serialized so the inspector displays a list and avoids any typos</param>
    /// <returns>Returns the unique instance of the sfx event.</returns>
    public static EventInstance PlaySFX(EventReference eventReference)
    {
        EventInstance sfx = RuntimeManager.CreateInstance(eventReference);
        sfx.start();
        return sfx;
    }
}

