using UnityEngine;
using UnityEngine.SceneManagement;

class Wall : MonoBehaviour
{
    public enum WallType { Left, Right, Top, Bottom }
    public WallType wallType;

    [Header("Bounce Settings")]
    private readonly float bounceMultiplier = 1f;

    [System.Obsolete]
    void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Rigidbody>(out var rb)) return;

        Vector3 currentVelocity = rb.velocity;
        Vector3 newVelocity = currentVelocity;

        switch (wallType)
        {
            case WallType.Left:
            case WallType.Right:
                // Mirror X velocity
                newVelocity.x = -currentVelocity.x * bounceMultiplier;
                break;

            case WallType.Top:
                // Mirror Y velocity  
                newVelocity.y = -currentVelocity.y * bounceMultiplier;
                break;

            case WallType.Bottom:
                // Player falls off the level
                if (other.tag == "Player") {
                    FallOff();
                }

                break;
        }

        rb.velocity = newVelocity;
    }

    void OnTriggerExit(Collider other)
    {
        // Terrible implementation of enemy despawning but hey it works
        if (other.tag == "SpawnedEnemy")
        {
            other.tag = "Enemy";
        } else if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }

    void FallOff()
    {
        // Game over handling upon falling off the level
        GameManager.Instance.GameOver();
    }
}

public class BoundaryController : MonoBehaviour
{
    GameObject leftWall;
    GameObject rightWall;
    GameObject topWall;
    GameObject bottomWall;

    [Header("Wall Settings")]
    public float wallThickness = 1f;
    public float wallDepth = 10f; // Depth for 3D colliders
    public string wallTag = "Boundary";
    public LayerMask wallLayer = 0;

    [Header("Padding")]
    public float padding = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateWalls();
        UpdateWallsPosition();
    }

    // Update is called once per frame
    void Update()
    {
        // Optional: Update walls if camera moves or changes size
        UpdateWallsPosition();
    }

    void CreateWalls()
    {
        // Create wall GameObjects
        leftWall = CreateWall(Wall.WallType.Left);
        rightWall = CreateWall(Wall.WallType.Right);
        topWall = CreateWall(Wall.WallType.Top);
        bottomWall = CreateWall(Wall.WallType.Bottom);
    }

    GameObject CreateWall(Wall.WallType wallType)
    {
        GameObject wall = new GameObject(wallType.ToString());
        wall.transform.SetParent(transform); // Make walls children of this controller

        // Add 3D collider
        BoxCollider collider = wall.AddComponent<BoxCollider>();
        collider.isTrigger = true;

        // Add 3D rigidbody (kinematic so they don't move)
        Rigidbody rb = wall.AddComponent<Rigidbody>();
        rb.isKinematic = true;


        wall.AddComponent<Wall>().wallType = wallType;

        // Set tag and layer
        if (!string.IsNullOrEmpty(wallTag))
            wall.tag = wallTag;

        if (wallLayer != 0)
            wall.layer = wallLayer;

        return wall;
    }

    void UpdateWallsPosition()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;

        // Get camera bounds in world coordinates
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        Vector3 cameraPosition = mainCamera.transform.position;

        // Calculate wall positions with padding
        float left = cameraPosition.x - cameraWidth / 2f - padding;
        float right = cameraPosition.x + cameraWidth / 2f + padding;
        float top = cameraPosition.y + cameraHeight / 2f + padding;
        float bottom = cameraPosition.y - cameraHeight / 2f - padding;

        // Position and scale the walls with 3D dimensions
        SetupWall(leftWall,
                 new Vector3(left - wallThickness / 2f, cameraPosition.y, cameraPosition.z),
                 new Vector3(wallThickness, cameraHeight + wallThickness * 2f, wallDepth));

        SetupWall(rightWall,
                 new Vector3(right + wallThickness / 2f, cameraPosition.y, cameraPosition.z),
                 new Vector3(wallThickness, cameraHeight + wallThickness * 2f, wallDepth));

        SetupWall(topWall,
                 new Vector3(cameraPosition.x, top + wallThickness / 2f, cameraPosition.z),
                 new Vector3(cameraWidth + wallThickness * 2f, wallThickness, wallDepth));

        SetupWall(bottomWall,
                 new Vector3(cameraPosition.x, bottom - wallThickness / 2f, cameraPosition.z),
                 new Vector3(cameraWidth + wallThickness * 2f, wallThickness, wallDepth));
    }

    void SetupWall(GameObject wall, Vector3 position, Vector3 scale)
    {
        if (wall == null) return;

        wall.transform.position = position;

        BoxCollider collider = wall.GetComponent<BoxCollider>();
        if (collider != null)
        {
            collider.size = Vector3.one; // Collider size is local to the object
            wall.transform.localScale = scale; // Use scale to set the actual size
        }
    }

    // Optional: Visualize walls in Scene view
    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.green;

        if (leftWall != null) Gizmos.DrawWireCube(leftWall.transform.position, leftWall.transform.localScale);
        if (rightWall != null) Gizmos.DrawWireCube(rightWall.transform.position, rightWall.transform.localScale);
        if (topWall != null) Gizmos.DrawWireCube(topWall.transform.position, topWall.transform.localScale);
        if (bottomWall != null) Gizmos.DrawWireCube(bottomWall.transform.position, bottomWall.transform.localScale);
    }
}