using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRamp : MonoBehaviour
{
    [SerializeField] GameObject initialRamp;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < 90; i++)
        {
            Transform initialRampTransform = initialRamp.GetComponent<Transform>();
            float newXPosition = initialRampTransform.position.x - (i * initialRampTransform.localScale.x);
            float newYPosition = initialRampTransform.position.y + i*.05f;
            Debug.Log($"new x: {newXPosition}, new y: {newYPosition}");
            GameObject nextRampPart;
            nextRampPart = Instantiate(initialRamp, new Vector3(newXPosition, newYPosition, initialRampTransform.position.z), Quaternion.Euler(new Vector3(0,0,-1*i)),this.transform);
            Debug.Log($"new x: {newXPosition}, new y: {newYPosition}");

            nextRampPart.name = $"ramp-at-degree-{i}";
        }
    }

}
