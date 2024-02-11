
using Normal.Realtime;
using UnityEngine;

public class BounceBall : MonoBehaviour
{
    [SerializeField] float followForce = 2f;
    [SerializeField] float maxSpeed = 2f;
    [SerializeField] float reflectForce = 5f;
    [SerializeField] float bounceForce = 5f;
    [SerializeField] float dragForce = 0.5f;
    [SerializeField] float maxDragSpeed = 5f;

    public RealtimeView realtimeView;

    public static UnityEngine.Vector3 pos;


    Rigidbody2D rb;
    Camera cam;
    Vector3 lastMousePos;
    Vector3 mousePos;
    Vector2 dir;
    float distance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        lastMousePos = Vector3.zero;
        mousePos = Vector3.zero;
        dir = Vector2.zero;
        distance = 0f;
    }

    void Update()
    {
        HandleDrag();

        pos = transform.position;
    }

    void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrag();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopDrag();
        }
    }

    float chargeTime = 0f;
    Vector2 mouseButtonAtStartOfDrag = Vector2.zero;
    Vector2 mouseButtonAtEndOfDrag = Vector2.zero;

    void StartDrag()
    {
        chargeTime += Time.deltaTime;
        mouseButtonAtStartOfDrag = Input.mousePosition;
        rb.velocity *= 1 - Time.deltaTime * 10f;
    }

    void StopDrag()
    {
        mouseButtonAtEndOfDrag = Input.mousePosition;
        dir = mouseButtonAtEndOfDrag - mouseButtonAtStartOfDrag;
        distance = dir.magnitude;

        //clamp char
        float clampedChargeTime = Mathf.Clamp(chargeTime, 0, maxDragSpeed);

        rb.AddForce(dir * clampedChargeTime * dragForce);
        ResetDragData();
    }

    void ResetDragData()
    {
        chargeTime = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;
        Vector2 newVelocity = Vector2.Reflect(rb.velocity, normal);
        newVelocity += normal * bounceForce;
        rb.velocity = newVelocity;
    }
}
