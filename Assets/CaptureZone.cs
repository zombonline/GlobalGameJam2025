using UnityEngine;

public class CaptureZone : MonoBehaviour
{
    [SerializeField] private float captureTime = 5f;
    [SerializeField] Team teamToCapture;
    int currentActivePlayers = 0;
    int initialActivePlayers = 0;
    float timer = 0;
    LevelManager levelManager;
    bool isCaptured = false;

    private void Awake()
    {
        levelManager = FindFirstObjectByType<LevelManager>();
        if(levelManager == null)
        {
            Debug.LogError("No LevelManager found in scene");
        }
    }
    private void Start()
    {
        initialActivePlayers = GameObject.FindGameObjectsWithTag(teamToCapture.ToString()).Length;
    }
    private void Update()
    {
        if (isCaptured) { return; }

        if (currentActivePlayers > 0) { timer += Time.deltaTime * (currentActivePlayers / initialActivePlayers);}
        else { timer -= Time.deltaTime; }

        if (timer >= captureTime)
        {
            isCaptured = true;
            levelManager.Win(teamToCapture);
        }
        timer = Mathf.Clamp(timer, 0, captureTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(teamToCapture.ToString()))
        {
            currentActivePlayers++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(teamToCapture.ToString()))
        {
            currentActivePlayers--;
        }
    }
}
