using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] float spinRate;

    private void Update()
    {
        transform.Rotate(Vector3.forward, spinRate * Time.deltaTime);
    }
}
