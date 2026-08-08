using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public float speed = 2f;

    private bool movingToPoint2 = true;

    void Update()
    {
        Transform target = movingToPoint2 ? point2 : point1;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            movingToPoint2 = !movingToPoint2;
        }
    }
}