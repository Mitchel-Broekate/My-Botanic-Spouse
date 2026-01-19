using UnityEngine;

public class XRMotionFootsteps : MonoBehaviour
{
    public Transform headTransform;
    public AudioSource audioSource;
    public AudioClip[] footstepClips;

    [Header("Footstep Settings")]
    public float stepDistance = 1.8f;
    public float minHorizontalSpeed = 0.15f;

    private Vector3 lastHeadPosition;
    private float distanceAccumulated;

    void Start()
    {
        lastHeadPosition = headTransform.position;
    }

    void Update()
    {
        Vector3 currentPosition = headTransform.position;

        // Ignore vertical movement
        Vector3 horizontalDelta = currentPosition - lastHeadPosition;
        horizontalDelta.y = 0f;

        float horizontalDistance = horizontalDelta.magnitude;
        float horizontalSpeed = horizontalDistance / Time.deltaTime;

        if (horizontalSpeed < minHorizontalSpeed)
        {
            lastHeadPosition = currentPosition;
            return;
        }

        distanceAccumulated += horizontalDistance;

        if (distanceAccumulated >= stepDistance)
        {
            PlayFootstep();
            distanceAccumulated = 0f;
        }

        lastHeadPosition = currentPosition;
    }

    void PlayFootstep()
    {
        if (footstepClips.Length == 0) return;

        audioSource.PlayOneShot(
            footstepClips[Random.Range(0, footstepClips.Length)]
        );
    }
}
