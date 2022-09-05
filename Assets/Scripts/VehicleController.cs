using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{

    [SerializeField] private Vector3 moveForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float steerAngle;
    [SerializeField] private float drag;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float tractionValue;


    // Update is called once per frame
    void Update()
    {
        VehicleMove();
        Drag();
        Traction();
    }


    private void VehicleMove()
    {
        moveForce += transform.forward * moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        transform.position += moveForce * Time.deltaTime;

        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerInput * moveForce.magnitude * steerAngle * Time.deltaTime);
    }

    private void Drag()
    {
        moveForce *= drag;
        moveForce = Vector3.ClampMagnitude(moveForce, maxSpeed);
    }

    private void Traction()
    {
        Debug.DrawRay(transform.position, moveForce.normalized * 3);
        Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
        moveForce = Vector3.Lerp(moveForce.normalized, transform.forward, tractionValue * Time.deltaTime) * moveForce.magnitude;
    }

}
