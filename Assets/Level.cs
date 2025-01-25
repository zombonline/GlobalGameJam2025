using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level : MonoBehaviour
{
    [SerializeField] public Transform[] bubbleSpawns, spikeSpawns;
    [SerializeField] public float timeLimit;
    [SerializeField] private float levelScale = 9f;

    [SerializeField] Camera mainCamera;
    [SerializeField] Transform outerWalls;
    [ExecuteInEditMode]

    private void OnValidate()
    {
        mainCamera.orthographicSize = levelScale;
        outerWalls.localScale = new Vector3(levelScale,levelScale,1);

    }


}
