using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deterrent_Effect : MonoBehaviour
{
    [SerializeField] float deterrentStrength = 50;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("#collision");
        if (col.gameObject.name == "Player")
        {
            //trigger bark animation
            Debug.Log($"#my position: {transform.position}");
            Debug.Log($"#colliders position: {col.transform.position}");
            Vector3 direction = transform.position - col.transform.position;
            Debug.Log($"#move away direction: {direction}");
            direction.Normalize();
            Debug.Log($"#normalized: {direction}");
            col.GetComponent<Rigidbody2D>().velocity = new Vector2(deterrentStrength, direction.y + 10);
            Debug.Log($"#new velocity: {col.GetComponent<Rigidbody2D>().velocity}");
        }
    }
}
