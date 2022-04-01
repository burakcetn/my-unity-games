using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private FieldOfView fieldOfView;

    // boundaries
    float xMin, yMin;
    float xMax, yMax;

    private Transform lightRotation;

    void Start()
    {
        SetUpMoveBoundaries();

        Initialize();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

    }

    void Update()
    {
        Move();

        Rotate();
    }

    private void Initialize()
    {
        lightRotation = transform.Find("Light Rotation");
    }

    private void Move()
    {
        var deltaX = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        var deltaY = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Rotate()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lightRotation.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        lightRotation.rotation = Quaternion.RotateTowards(lightRotation.rotation, rotation, 360);

        fieldOfView.SetOrigin(transform.position);

        fieldOfView.SetAimDirection(direction);
    }

}
