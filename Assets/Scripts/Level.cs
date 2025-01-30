using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level : MonoBehaviour
{
    [SerializeField] Transform bubbleSpawnsParent, spikeSpawnsParent;
    private List<Transform> bubbleSpawns, spikeSpawns;
    [SerializeField] private float timeLimit;
    [SerializeField] private float levelScale = 9f;

    [SerializeField] Camera mainCamera;
    [SerializeField] SpriteRenderer background;
    [SerializeField] Transform levelWalls;

    public bool levelComplete = false;

    [ExecuteInEditMode]
    private void OnValidate()
    {
        mainCamera.orthographicSize = levelScale;
        GetComponent<BoxCollider2D>().size = new Vector2(mainCamera.ScreenToWorldPoint(mainCamera.pixelRect.size).x*2, mainCamera.ScreenToWorldPoint(mainCamera.pixelRect.size).y*2);
        background.size = new Vector2(mainCamera.ScreenToWorldPoint(mainCamera.pixelRect.size).x * 2, mainCamera.ScreenToWorldPoint(mainCamera.pixelRect.size).y * 2);
        levelWalls.localScale = new Vector3(levelScale, levelScale, 1f);
    }

    private void Awake()
    {
        PopulateSpawnsLists();
    }

    private void PopulateSpawnsLists()
    {
        bubbleSpawns = new List<Transform>();
        spikeSpawns = new List<Transform>();
        foreach (Transform spawn in bubbleSpawnsParent)
        {
            bubbleSpawns.Add(spawn);
        }
        foreach (Transform spawn in spikeSpawnsParent)
        {
            spikeSpawns.Add(spawn);
        }
    }

    /// <summary>
    /// Returns a random spawn position for the given team and removes it from the list of available spawns, so it won't be used again.
    /// </summary>
    /// <param name="team">Takes a reference to the team to decide what kind of spawn should be returned.</param>
    /// <returns>Returns a Vector2 with the position of the spawn.</returns>
    public Vector2 GetRandomSpawnPosition(Team team)
    {
        Transform spawn;
        if(team == Team.Bubble)
        {
            spawn = bubbleSpawns[Random.Range(0, bubbleSpawns.Count)];
            bubbleSpawns.Remove(spawn);
        }
        else
        {
            spawn = spikeSpawns[Random.Range(0, spikeSpawns.Count)];
            spikeSpawns.Remove(spawn);
        }
        return spawn.position;
    }
    public float GetTimeLimit()
    {
        return timeLimit;
    }

    public Bounds GetLevelBounds()
    {
        return GetComponent<BoxCollider2D>().GetComponent<Collider2D>().bounds;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            collision.GetComponent<PlayerMovement>().SetPositionToLastFrame();
        }
    }
}
