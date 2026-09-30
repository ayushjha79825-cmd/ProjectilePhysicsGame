using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [Header("References")]
    public Rigidbody projectile;
    public ParticleSystem launchEffect;

    [Header("Launch Settings")]
    public float forceMultiplier = 5f;

    [Header("Trajectory")]
    public LineRenderer aimLine;
    public int linePoints = 30;
    public float timeStep = 0.05f;

    private Vector3 dragStart;
    private bool dragging;
    private bool launched;

    void Start()
    {
        aimLine.positionCount = linePoints;
        aimLine.enabled = false;

        Gradient redGradient = new Gradient();
        redGradient.SetKeys(
            new GradientColorKey[]
            {
                new GradientColorKey(Color.red, 0f),
                new GradientColorKey(Color.red, 1f)
            },
            new GradientAlphaKey[]
            {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(1f, 1f)
            }
        );

        aimLine.colorGradient = redGradient;
    }

    void Update()
    {
        if (launched)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            StartDrag(Input.mousePosition);
        }

        if (Input.GetMouseButton(0) && dragging)
        {
            UpdateDrag(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0) && dragging)
        {
            ReleaseProjectile(Input.mousePosition);
        }
    }

    void StartDrag(Vector3 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.rigidbody == projectile)
            {
                dragStart = screenPosition;
                dragging = true;
                aimLine.enabled = true;
            }
        }
    }

    // Helper method: Converts Screen Drag into World Direction
    Vector3 CalculateWorldDirection(Vector3 drag)
    {
        // Camera directions relative to world
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        // Ground plane flat directions (no upward rotation tilt)
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        // Screen X drives Left/Right (camRight)
        // Screen Y drives Forward/Backward (camForward)
        // Upward arc (Y) can be added as an offset if needed
        Vector3 direction = (camRight * drag.x) + (camForward * drag.y);

        // Agar aap chahte ho ki Drag upar karne se thoda height angle (arc) bhi bane:
        direction.y = drag.y * 0.5f;

        return direction;
    }

    void UpdateDrag(Vector3 currentPosition)
    {
        Vector3 drag = dragStart - currentPosition;

        Vector3 direction = CalculateWorldDirection(drag);

        if (direction.sqrMagnitude > 0.001f)
        {
            direction.Normalize();
        }

        float force = drag.magnitude * forceMultiplier * 0.01f;

        Vector3 velocity = direction * force;

        DrawTrajectory(velocity);
    }

    void DrawTrajectory(Vector3 velocity)
    {
        Vector3 startPosition = projectile.position;

        for (int i = 0; i < linePoints; i++)
        {
            float time = i * timeStep;

            Vector3 position =
                startPosition +
                velocity * time +
                0.5f * Physics.gravity * time * time;

            aimLine.SetPosition(i, position);
        }
    }

    void ReleaseProjectile(Vector3 releasePosition)
    {
        Vector3 drag = dragStart - releasePosition;

        Vector3 direction = CalculateWorldDirection(drag);

        if (direction.sqrMagnitude > 0.001f)
        {
            direction.Normalize();
        }
        else
        {
            dragging = false;
            aimLine.enabled = false;
            return;
        }

        float force = drag.magnitude * forceMultiplier * 0.01f;

        aimLine.enabled = false;
        dragging = false;

        if (launchEffect != null)
        {
            launchEffect.Play();
        }

        projectile.AddForce(direction * force, ForceMode.Impulse);

        SurfaceInteraction surfaceInteraction = projectile.GetComponent<SurfaceInteraction>();

        if (surfaceInteraction != null)
        {
            surfaceInteraction.StartChecking();
        }

        launched = true;
    }
}