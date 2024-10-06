using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Collider bladeCollider;
    private Camera mainCamera;
    private bool slicing;
    private TrailRenderer bladeTrail;
    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;

    public Vector3 direction {  get; private set; }
    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();

        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (slicing)
        {
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        transform.position = newPosition;

        slicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled=false;
    }

    private void ContinueSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        direction = newPosition - transform.position; //direction blade is moving in

        float velocity = direction.magnitude / Time.deltaTime; // check velocity to see if we want to disable blade collider if not moving 
        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition; // use curr position and last thing update new
    }
}
