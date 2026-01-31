using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuoancyObject : MonoBehaviour
{
    public Transform[] floaters;

    

    public float UndeWaterDrag = 3f;
    public float UndeWaterAngularDrag = 1f;

    public float AirDrag = 0f;
    public float AirAngularDrag = 0.05f;

    public float FloatPower = 100f;

    public float WaterHeight = 0f;

    Rigidbody Rigidbody;

    int FloatersUndewater;
    bool underwater;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        FloatersUndewater = 0;
        for (int i = 0; i < floaters.Length; i++)
        {
            float difference = floaters[i].position.y - WaterHeight;
            if (difference < 0)
            {
                Rigidbody.AddForceAtPosition(Vector3.up * FloatPower * Mathf.Abs(difference), floaters[i].position, ForceMode.Force);
                FloatersUndewater += 1;
                if (!underwater)
                {
                    underwater = true;
                    SwitchState(true);
                }
            }
            if (underwater && FloatersUndewater == 0)
            {
                underwater = false;
                SwitchState(false);
            }
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
