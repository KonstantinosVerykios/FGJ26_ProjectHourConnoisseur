using UnityEngine;

public class MovingFog : MonoBehaviour
{
    public GameObject boat;

    public Vector3 fogOffset;
    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = boat.transform.position + fogOffset;
    }
}
