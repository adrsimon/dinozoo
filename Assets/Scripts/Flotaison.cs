using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Flotaison : MonoBehaviour
{
    [SerializeField]
    float buoyantForce = 8f;
    [SerializeField,
    Range(0f, 1f)]
    float depthPower = 1f;
    [SerializeField]
    float offsetY = 0f;
    [SerializeField]
    string waterVolumeTag = "Water";
    [SerializeField]
    float angularDragInWater = 0.5f;
    [SerializeField]
    float waterDamping = 1f;


    private Rigidbody rb;
    private Collider coll;
    private WaterBody waterBody;
    private float yBound;
    private bool isWaterBodySet;
    private int waterCount;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        if (waterCount == 0)
        {
            waterBody = null;
            isWaterBodySet = false;
        }
    }

    public void SetDepthPower(in float value)
    {
        if (value >= 0f && value <= 1f) depthPower = value;
    }
    public float GetDepthPower() => depthPower;
    public bool IsUnderWater() => isWaterBodySet && yBound > coll.bounds.max.y;
    public bool IsFloating() => isWaterBodySet && !(yBound > coll.bounds.max.y);

    private void OnTriggerEnter(Collider water)
    {
        if (water.CompareTag(waterVolumeTag)) waterCount++;
    }

    private void OnTriggerStay(Collider water)
    {
        if (water.CompareTag(waterVolumeTag))
        {
            if (transform.position.x < water.bounds.max.x
            && transform.position.z < water.bounds.max.z
            && transform.position.x > water.bounds.min.x
            && transform.position.z > water.bounds.min.z)
            {
                if (waterBody != null && !ReferenceEquals(waterBody.gameObject, water.gameObject))
                {
                    waterBody = null;
                    isWaterBodySet = false;
                }

                if (!isWaterBodySet)
                {
                    waterBody = water.GetComponent<WaterBody>();
                    if (waterBody != null) isWaterBodySet = true;
                }
                else
                {
                    float objectYValue = coll.bounds.center.y + offsetY;
                    yBound = waterBody.GetYBound();

                    if (objectYValue < yBound)
                    {
                        float depth = yBound - objectYValue;

                        float buoyancyForce = buoyantForce * Physics.gravity.magnitude * coll.bounds.size.x * coll.bounds.size.z * Mathf.Clamp01(depth / coll.bounds.size.y);
                        rb.AddForce(0f, buoyancyForce, 0f);
                        rb.AddForce(-rb.velocity * waterDamping);
                        rb.angularDrag = angularDragInWater;
                    }
                    else
                    {
                        rb.angularVelocity = Vector3.zero;
                    }
                }
            }
        }
    }


    private void OnTriggerExit(Collider water)
    {
        if (water.CompareTag(waterVolumeTag)) waterCount--;
    }
}