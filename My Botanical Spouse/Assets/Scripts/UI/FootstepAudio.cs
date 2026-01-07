using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class XRMotionFootsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepClips;

    [Header("Footstep Settings")]
    public float stepDistance = 1.8f;
    public float minHorizontalSpeed = 0.15f;

    private CharacterController controller;
    private Vector3 lastPosition;
    private float distanceAccumulated;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;

        // Ignore vertical movement completely
        Vector3 horizontalDelta = currentPosition - lastPosition;
        horizontalDelta.y = 0f;

        float horizontalDistance = horizontalDelta.magnitude;
        float horizontalSpeed = horizontalDistance / Time.deltaTime;

        if (horizontalSpeed < minHorizontalSpeed)
        {
            lastPosition = currentPosition;
            return;
        }

        distanceAccumulated += horizontalDistance;

        if (distanceAccumulated >= stepDistance)
        {
            PlayFootstep();
            distanceAccumulated = 0f;
        }

        lastPosition = currentPosition;
    }

    void PlayFootstep()
    {
        if (footstepClips.Length == 0) return;

        audioSource.PlayOneShot(
            footstepClips[Random.Range(0, footstepClips.Length)]
        );
    }
}
