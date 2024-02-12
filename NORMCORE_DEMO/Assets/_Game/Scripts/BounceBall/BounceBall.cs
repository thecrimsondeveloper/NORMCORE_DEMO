
using Normal.Realtime;
using UnityEngine;

public class BounceBall : RealtimeComponent<BounceBallModel>
{
    [SerializeField] float followForce = 2f;
    [SerializeField] float maxSpeed = 2f;
    [SerializeField] float reflectForce = 5f;
    [SerializeField] float randomForce = 5f;
    [SerializeField] float bounceForce = 5f;
    [SerializeField] float dragForce = 0.5f;
    [SerializeField] float maxDragSpeed = 5f;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] LayerMask layer;

    public PlayerColor color => model != null ? model.color : PlayerColor.Default;

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

    private void FixedUpdate()
    {
        //clamp the velocity of the ball
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
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
        Vector2 initialVelocity = rb.velocity;
        Vector2 normal = collision.contacts[0].normal;
        Vector2 reflection = Vector2.Reflect(initialVelocity, normal);
        Vector2 bounce = normal * bounceForce;

        Vector2 newVelocity = reflection + bounce;
        float dotBetweenNormal = Vector2.Dot(initialVelocity.normalized, normal);
        float dotBetweenReflection = Vector2.Dot(initialVelocity.normalized, reflection.normalized);






        //if the reflection and the velcoty are in the same general direction then we want to multipply by the dot product
        //this will make the ball continue bouncing in the same direction
        Debug.Log("Dot: " + dotBetweenReflection + " " + dotBetweenNormal);
        if (dotBetweenReflection > 0)
        {
            //use the base reflection and add the reflection * the dot product * the reflect force so that the ball continues to bounce in the same direction
            newVelocity += reflection * dotBetweenReflection * reflectForce;
        }
        else
        {
            newVelocity += Random.insideUnitCircle * randomForce;
            Debug.Log("Random");
        }

        rb.velocity = newVelocity;

        //if the collision is the layer we want to bounce off of
        if (layer == (layer | (1 << collision.gameObject.layer)))
        {
            //this means we hit the arena
            Arena.SetColorOfClosestTile(transform.position, color);
        }
    }


    public void SetColor(PlayerColor color)
    {
        if (model != null)
            model.color = color;
    }

    void ColorDidChange(BounceBallModel model, PlayerColor value)
    {
        Debug.Log("Color changed to: " + value);
        sprite.color = Player.GetColor(value);
    }


    protected override void OnRealtimeModelReplaced(BounceBallModel previousModel, BounceBallModel currentModel)
    {

        Debug.Log("OnRealtimeModelReplaced");
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.colorDidChange -= ColorDidChange;
        }

        if (currentModel != null)
        {
            // Update the sprite renderer to match the new model
            ColorDidChange(currentModel, currentModel._color);
            currentModel.colorDidChange += ColorDidChange;
        }
    }
}
