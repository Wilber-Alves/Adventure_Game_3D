using UnityEngine;

public class ItemCollectableAnimator : MonoBehaviour
{

    [Tooltip("The speed at which the coin bobs up and down.")]
    public float bounceSpeed = 2.0f;
    [Tooltip("The total height of the bobbing motion.")]
    public float bounceAmount = 0.5f;

    [Tooltip("The speed at which the coin rotates on the Y-axis (degrees per second).")]
    public float rotationSpeed = 180.0f;

    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
       
        Floating();
        Rotation();
    }

    private void Floating()
    {
        float yOffset = Mathf.Sin(Time.time * bounceSpeed) * bounceAmount;
        transform.position = new Vector2(startPosition.x, startPosition.y + yOffset);
    }

    private void Rotation()
    {
        transform.Rotate(Vector2.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
