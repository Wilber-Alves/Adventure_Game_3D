using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public float distance = 0.2f;
    public float coinSpeed = 10f;

    void Update()
    {
       if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) > distance)
       {
            coinSpeed += 1f;
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.Instance.transform.position, Time.deltaTime * coinSpeed);
       }

    }
}
