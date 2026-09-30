using UnityEngine;

public class SurfaceInteraction : MonoBehaviour
{
    [Header("Launch Timer")]
    public float maxTime = 5f;

    [Header("Stuck Detection")]
    public float stuckTimeLimit = 1.5f;
    public float minimumMovement = 0.03f;

    [Header("Game Manager")]
    public GameManager gameManager;

    private Rigidbody rb;

    private bool hasLaunched;
    private float launchTimer;
    private float stuckTime;

    private Vector3 lastPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
    }

    public void StartChecking()
    {
        hasLaunched = true;

        launchTimer = 0f;
        stuckTime = 0f;

        lastPosition = transform.position;
    }

    void Update()
    {
        if (!hasLaunched || gameManager == null || gameManager.gameOver)
            return;

        // Timer starts after ball is released
        launchTimer += Time.deltaTime;

        // If target is not reached within 5 seconds
        if (launchTimer >= maxTime)
        {
            FailBall();
            return;
        }

        CheckIfStuck();
    }

    void CheckIfStuck()
    {
        float distanceMoved = Vector3.Distance(
            transform.position,
            lastPosition
        );

        if (distanceMoved < minimumMovement)
        {
            stuckTime += Time.deltaTime;

            if (stuckTime >= stuckTimeLimit)
            {
                FailBall();
                return;
            }
        }
        else
        {
            stuckTime = 0f;
        }

        lastPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasLaunched || gameManager == null || gameManager.gameOver)
            return;

        // Target = SUCCESS
        if (collision.gameObject.name == "Target" ||
            collision.gameObject.name == "GoalMaker")
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            gameManager.Success();
            return;
        }

        // Path collision does NOTHING.
        // The ball can bounce/continue from the Path.
    }

    void FailBall()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        gameManager.Fail();
    }
}