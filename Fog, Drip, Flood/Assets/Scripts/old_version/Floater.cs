using UnityEngine;

public class Floater : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float depthBeforeSubmerge = 1f;
    public float waterDisplaceAmount = 3f;

    public int floaterCount = 1;

    public float waterDrag = 0.99f;
    public float waterAngularDrag = 0.5f;
    // Update is called once per frame
    void FixedUpdate()
    {
        //REMEMBER TO UPDATE FLOATER COUNT
        rigidBody.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);

        float waveHeight = WaveManager.instance.GetWaveHeight(transform.position.x);

        if(transform.position.y < waveHeight)
        {
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubmerge) * waterDisplaceAmount;

            rigidBody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);

            rigidBody.AddForce(displacementMultiplier * -rigidBody.linearVelocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);

            rigidBody.AddTorque(displacementMultiplier * -rigidBody.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
