using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BorderColliders : MonoBehaviour
{
    public bool setOnUpdate = false; // Determines if positions should be set on Update

    private Camera mainCamera;
    private BoxCollider2D[] colliders;

    void Awake()
    {

        if (colliders == null || colliders.Length != 4)
        {
            colliders = GetComponents<BoxCollider2D>();
        }

        if (colliders == null || colliders.Length != 4)
        {
            //destroy all colliders that are currently attached to the gameobject
            foreach (BoxCollider2D collider in GetComponents<BoxCollider2D>())
            {
                Destroy(collider);
            }

            colliders = new BoxCollider2D[4];
            for (int i = 0; i < 4; i++)
            {
                colliders[i] = gameObject.AddComponent<BoxCollider2D>();
            }
        }
    }





    void Start()
    {
        if (!setOnUpdate)
            SetBorderPositions();
    }

    void Update()
    {
        if (setOnUpdate)
            SetBorderPositions();


        if (transform.position.magnitude > 10f)
        {
            transform.position = Vector3.zero;
        }
    }

    void SetBorderPositions()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }

        // Calculate the world coordinates of the screen borders
        float screenHeight = 2f * mainCamera.orthographicSize;
        float screenWidth = screenHeight * mainCamera.aspect;
        Vector3 cameraPos = mainCamera.transform.position;

        // Calculate the world coordinates of the screen borders
        Vector2 topLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, -cameraPos.z));
        Vector2 bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, -cameraPos.z));

        // Position the colliders to create a border around the screen
        colliders[0].size = new Vector2(bottomRight.x - topLeft.x, 1f);
        colliders[0].offset = new Vector2(cameraPos.x, topLeft.y + 0.5f); // Top border

        colliders[1].size = new Vector2(bottomRight.x - topLeft.x, 1f);
        colliders[1].offset = new Vector2(cameraPos.x, bottomRight.y - 0.5f); // Bottom border

        colliders[2].size = new Vector2(1f, topLeft.y - bottomRight.y);
        colliders[2].offset = new Vector2(topLeft.x - 0.5f, cameraPos.y); // Left border

        colliders[3].size = new Vector2(1f, topLeft.y - bottomRight.y);
        colliders[3].offset = new Vector2(bottomRight.x + 0.5f, cameraPos.y); // Right border
    }

    //on collision reflect the ball


}
