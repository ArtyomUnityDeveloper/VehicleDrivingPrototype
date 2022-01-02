using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Using this variable to determine which player is using the script
    public string inputID;


    [SerializeField] float speed = 20.0f;
    [SerializeField] float turnSpeed = 45.0f;
    private float horizontalInput;
    private float forwardInput;
    private Rigidbody playerRb;
    [SerializeField] private float horsePower;
    [SerializeField] private GameObject settedCentreOfMass;

    // For camera view switch
    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode switchKey;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] float calculatedSpeed;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] float rpm;

    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = settedCentreOfMass.transform.localPosition;
    }

    private void Update()
    {
        calculatedSpeed = Mathf.Round(playerRb.velocity.magnitude * 3.6f);
        speedometerText.SetText("Speed: " + calculatedSpeed + "kph");
        rpm = Mathf.Round((calculatedSpeed % 30) * 40);
        rpmText.SetText("RPM: " + rpm);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal" + inputID);
        forwardInput = Input.GetAxis("Vertical" + inputID);

        /* // Moves the vehicle forward based on vertical input
         //transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
         playerRb.AddRelativeForce(Vector3.forward * forwardInput * horsePower);
         // Rotates the vehicle based on horizontal input
         transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime); */

        if (IsOnGround())
        {

            //Move the vehicle forward
            //transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
            playerRb.AddRelativeForce(Vector3.forward * forwardInput * horsePower);
            //Turning the vehicle
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);

            //print speed
            speed = Mathf.RoundToInt(playerRb.velocity.magnitude * 2.237f);
            speedometerText.SetText("Speed: " + speed + " mph");

            //print RPM
            rpm = Mathf.Round((speed % 30) * 40);
            rpmText.SetText("RPM: " + rpm);
        }

        //This code will toggle which camera is enabled when the F key is pressed.
        if (Input.GetKeyDown(switchKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }
    }

    bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }
        if (wheelsOnGround == 4)
        {
            //Debug.Log(gameObject.name + " wheels on ground " + wheelsOnGround);
            return true;
        }
        else
        {
            //Debug.Log(gameObject.name + " wheels on ground " + wheelsOnGround);
            return false;
        }
    }
}
