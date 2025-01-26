using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{
    Transform point1, point2;
    [SerializeField] float speed;
    [SerializeField] GameObject objectToMove;

    private void Awake()
    {
        point1 = transform.GetChild(0);
        point2 = transform.GetChild(1);
    }

    private void Update()
    {
        objectToMove.transform.position = Vector3.Lerp(point1.position, point2.position, Mathf.PingPong(Time.time * speed, 1));
    }
}
