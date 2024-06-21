using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        // Get input from the horizontal and vertical axes
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        // Calculate movement direction based on input
        Vector3 movement = new Vector3(horizontalAxis, 0.0f, verticalAxis) * speed * Time.deltaTime;

        // Move the object
        transform.Translate(movement);
    }
}
