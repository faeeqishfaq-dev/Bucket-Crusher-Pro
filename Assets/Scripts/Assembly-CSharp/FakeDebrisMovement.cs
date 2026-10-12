using UnityEngine;

public class FakeDebrisMovement : MonoBehaviour
{
    private Transform entry;
    private Transform slope;
    private Transform consume;

    private int stage = 1;
    private VoxelWallConverter converter;
    private Vector3 velocity;
    private float attractionRadius = 2.5f;

    public void Init(Vector3 startPos, VoxelWallConverter conv)
    {
        transform.position = startPos;
        converter = conv;
        stage = 1;

        if (entry == null)
        {
            var magnet = Object.FindAnyObjectByType<MachineIntakeMagnet>();
            if (magnet != null)
            {
                entry = magnet.intakeEntryPoint;
                slope = magnet.intakeSlopePoint;
                consume = magnet.intakeConsumePoint;
                attractionRadius = magnet.attractionRadius;
            }
        }
        
        // Apply similar initial velocity as real bricks
        velocity = new Vector3(
            Random.Range(-3.0f, 3.0f),
            Random.Range(0f, 2.0f),
            Random.Range(-1.0f, 1.0f)
        );
    }

    private void Update()
    {
        if (entry == null)
        {
            // Fallback if no magnet found
            velocity.y -= 9.81f * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
            if (transform.position.y < -10f)
            {
                if (converter != null) converter.ReturnFakeBrickToPool(gameObject);
                else Destroy(gameObject);
            }
            return;
        }
        
        Vector3 targetPos = Vector3.zero;
        if (stage == 1)
        {
            targetPos = entry.position;
            if (Vector3.Distance(transform.position, targetPos) < 0.4f) stage = 2;
        }
        else if (stage == 2)
        {
            targetPos = slope.position;
            if (Vector3.Distance(transform.position, targetPos) < 0.3f) stage = 3;
        }
        else if (stage == 3)
        {
            targetPos = consume.position;
            if (Vector3.Distance(transform.position, targetPos) < 0.2f)
            {
                if (converter != null) converter.ReturnFakeBrickToPool(gameObject);
                else Destroy(gameObject);
                return;
            }
        }

        Vector3 direction = targetPos - transform.position;
        float distance = direction.magnitude;

        if (distance > attractionRadius + 0.5f)
        {
            // Outside magnet, apply gravity
            velocity.y -= 9.81f * Time.deltaTime;
        }
        else
        {
            // Inside magnet, steer towards target
            if (distance > 0.05f)
            {
                Vector3 dirNorm = direction.normalized;
                // Magnet strength increases as stage progresses to simulate real intake
                float pullSpeed = stage == 1 ? 7f : 12f;
                velocity = Vector3.Lerp(velocity, dirNorm * pullSpeed, Time.deltaTime * 6f);
            }
        }
        
        transform.position += velocity * Time.deltaTime;
        
        // Simulate tumbling
        transform.Rotate(velocity * (Time.deltaTime * 45f), Space.World);
    }
}
