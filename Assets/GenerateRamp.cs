using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRamp : MonoBehaviour
{
    [SerializeField] GameObject initialRamp;
    // Start is called before the first frame update
    void Start()
    {
        Transform initialRampTransform = initialRamp.GetComponent<Transform>();

        //Debug.Log($"Initial x positon: {initialRampTransform.position.x}");
        float xOffsetTotal = initialRampTransform.position.x;
        for (int i = 1; i < 90; i++)
        {
            float currXRatio = ((90f-i)/90f);
            //Debug.Log($"xratio: {currXRatio}");
            float xOffset = currXRatio * initialRampTransform.localScale.x;
            //Debug.Log($"xoffset: {xOffset}");
            xOffsetTotal += xOffset;
            float newXPosition = initialRampTransform.position.x - (xOffsetTotal);

            float currYRatio = (i/90f);
            Debug.Log($"yratio: {currYRatio}");
            float yOffset = (i*i)*currYRatio;
            Debug.Log($"yoffset, i/ratio: {yOffset}");
            float newYPosition = initialRampTransform.position.y + yOffset;
            Debug.Log($"new x: {newXPosition}, new y: {newYPosition}");
            GameObject nextRampPart;
            nextRampPart = Instantiate(initialRamp, new Vector3(newXPosition, newYPosition, initialRampTransform.position.z), Quaternion.Euler(new Vector3(0,0,-1*i)),this.transform);

            nextRampPart.name = $"ramp-at-degree-{i}";
        }
    }

}
