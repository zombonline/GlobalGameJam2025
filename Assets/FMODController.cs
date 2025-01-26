using UnityEngine;
using FMOD;
using FMODUnity;
public class FMODController : MonoBehaviour
{
    [SerializeField] StudioEventEmitter fmodEmitter;

    public void UpdateState(string name)
    {
        fmodEmitter.Play();
    }
}
