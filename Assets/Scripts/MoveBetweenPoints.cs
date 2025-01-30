using UnityEngine;

[System.Serializable]
enum StartPoint { Point1, Point2 }
[ExecuteInEditMode]
public class MoveBetweenPoints : MonoBehaviour
{
    Transform point1, point2;
    [SerializeField] float speed;
    [SerializeField] GameObject objectToMove;
    [SerializeField] StartPoint startPoint;
    private void Awake()
    {
        if (objectToMove == null) { return; }
        point1 = transform.GetChild(0);
        point2 = transform.GetChild(1);
        objectToMove.transform.position = startPoint == StartPoint.Point1 ? point1.position : point2.position;
    }
    private void OnValidate()
    {
        if(objectToMove == null) { return; }
        point1 = transform.GetChild(0);
        point2 = transform.GetChild(1);
        objectToMove.transform.position = startPoint == StartPoint.Point1 ? point1.position : point2.position;
    }
    private void Update()
    {
        if (!Application.isPlaying) { return; }
        objectToMove.transform.position = Vector3.Lerp(point1.position, point2.position, Mathf.PingPong(Time.time * speed, 1));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.GetChild(0).position, transform.GetChild(1).position);
    }
}
