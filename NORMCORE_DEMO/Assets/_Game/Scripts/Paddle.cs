using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using Normal.Realtime;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] RealtimeView realtime;
    [SerializeField] RealtimeTransform realtimeTransform;

    Camera mainCamera;

    void Start()
    {
        //if is local player
        if (realtime.isOwnedLocallyInHierarchy)
        {
            //set the main camera to the camera in the scene
            mainCamera = Camera.main;

            //lock the cursor to the center of the screen
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            //request ownership of the realtime transform
            realtimeTransform.RequestOwnership();
        }
        else
        {
            enabled = false;
        }



        mainCamera = Camera.main;

        //lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }


    Vector2 mouseDelta = Vector2.zero;
    Vector2 lastMousePos = Vector2.zero;

    void FixedUpdate()
    {
        //get horizontal and vertical input
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //add force to the rigidbody
        rb.AddForce(new Vector2(x, y) * speed);

        //clamp the velocity of the rigidbody
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);

        //if the magnitude of the velocity is greater than 0.1f
        if (rb.velocity.magnitude > 0f)
        {


            //get the direction from the mouse to the player
            Vector2 dir = BounceBall.pos - transform.position;

            //get the angle of the direction
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            //set the rotation of the player to the angle
            rb.rotation = angle;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collided with " + other.gameObject.name);
        if (other.gameObject.TryGetComponent(out BounceBall ball))
        {
            Debug.Log("Collided with ball");
            //set the owner of the ball to the local player
            ball.realtimeView.RequestOwnershipOfSelfAndChildren();
        }
    }
}

