using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuoancyObject : MonoBehaviour
{

    public float UndeWaterDrag = 3f;
    public float UndeWaterAngularDrag = 1f;

    public float AirDrag = 0f;
    public float AirAngularDrag = 0.05f;

    public float FloatPower = 100f;

    public float WaterHeight = 0f;

    Rigidbody Rigidbody;

    bool underwater;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float difference = transform.position.y - WaterHeight;

        if(difference < 0)
        {
            Rigidbody.AddForceAtPosition(Vector3.up * FloatPower * Mathf.Abs(difference), transform.position, ForceMode.Force);
            if (!underwater)
            {
                underwater = true;
                SwitchState(true);
            }
        }
        else if (underwater){
            underwater = false;
            SwitchState(false);
        }
    }

    void SwitchState(bool isUnderwater)
    {
        if (isUnderwater)
        {
            Rigidbody.linearDamping = UndeWaterDrag;
            Rigidbody.angularDamping = UndeWaterAngularDrag;
        }
        else
        {
            Rigidbody.linearDamping = AirDrag;
            Rigidbody.angularDamping = AirAngularDrag;
        }
    }
}
