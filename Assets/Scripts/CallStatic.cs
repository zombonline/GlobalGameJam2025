using UnityEngine;

public class CallStatic : MonoBehaviour
{
    public void FmodController_InitiateMusic()
    {
        FMODController.InitiateMusic();
    }
    public void FmodController_UpdateMusicState(int val)
    {
        FMODController.UpdateMusicState(val);
    }
}
