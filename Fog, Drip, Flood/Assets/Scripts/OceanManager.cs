using UnityEngine;

public class OceanManager : MonoBehaviour
{
    public float wavesHeigth = 0.2f;

    public float wavesFrequency = 0.4f;

    public float wavesSpeed = 0.03f;

    public Transform ocean;

    Material oceanMat;

    Texture2D wavesDisplacement;

    void Start()
    {
        SetVariables();
    }

    void SetVariables()
    {
        oceanMat = ocean.GetComponent<Renderer>().sharedMaterial;
        wavesDisplacement = (Texture2D)oceanMat.GetTexture("_WaveDisplacement");
    }

    public float WaterHeightAtPosition(Vector3 position)
    {
        return ocean.position.y * wavesDisplacement.GetPixelBilinear(position.x * wavesFrequency, Time.time * wavesSpeed).g * wavesHeigth * ocean.localScale.x;
    }

    void OnValidate()
    {
        if (!oceanMat)
        {
            SetVariables();
        }
        UpdateMaterial();
    }

    void UpdateMaterial()
    {
        oceanMat.SetFloat("_WaveFrequency", wavesFrequency);
        oceanMat.SetFloat("_WaveSpeed", wavesSpeed);
        oceanMat.SetFloat("_WaveHeight", wavesHeigth);
    }
}
