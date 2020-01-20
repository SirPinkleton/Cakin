using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRamp : MonoBehaviour
{
    [SerializeField] GameObject initialRamp;
    [SerializeField] float xPositionAdjustment = -10;
    // Start is called before the first frame update
    void Start()
    {
        initialRamp = gameObject;

        for (int i = 1; i < 90; i++)
        {
            Transform initialRampTransform = initialRamp.GetComponent<Transform>();
            float newXPosition = initialRampTransform.position.x + (i * xPositionAdjustment);
            float newYPosition = initialRampTransform.position.y + i;

            GameObject nextRampPart;
            nextRampPart = Instantiate(initialRamp, new Vector3(newXPosition, newYPosition, initialRampTransform.position.z), new Quaternion(0,0,(-1*i),0));

            nextRampPart.name = $"ramp-at-degree-{i}";
        }
    }

}
