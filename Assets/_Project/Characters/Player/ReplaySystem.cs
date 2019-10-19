using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaySystem : MonoBehaviour 
{
    #region Fields
    private const int recordingBufferFrames = 300;
    private Queue<Keyframe> keyframes = new Queue<Keyframe>(recordingBufferFrames);

    private GameManager gameManager;

    private Rigidbody rb;
    #endregion

    // Use this for initialization
    void Start () 
	{
        gameManager = GameObject.FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
        Debug.AssertFormat(gameManager, "{0}: No GameManager found.", this);
	}
	
	// Update is called once per frame
	void Update()
    {
        if (gameManager.Recording)
        {
            Record();
        }
        else
        {
            Play();
        }
    }

    /// <summary>
    /// Replay system. 
    /// Records to a Queue.
    /// Keeps max. recordingBufferFrames items in the Queue.
    /// </summary>
    private void Record ()
    {
        rb.isKinematic = false;

        float time = Time.time;

        // Add new keyframe at the end of the Queue.
        // Store parameters this frame.
        keyframes.Enqueue(new Keyframe(time, transform.position, transform.rotation));

        // Prevent queue from storing more items than recordingBufferFrames.
        if (keyframes.Count > recordingBufferFrames)
        {
            // Discard first (oldest) item.
            keyframes.Dequeue();
        }
    }

    /// <summary>
    /// Plays back the queue.
    /// Deletes played frames (one-time replay).
    /// </summary>
    private void Play ()
    {
        // Stored keyframes are available.
        if (keyframes.Count > 0)
        {
            rb.isKinematic = true;

            // Return oldest keyframe and remove from Queue.
            Keyframe currentFrame = keyframes.Dequeue();

            // Apply returned parameters to object.
            transform.position = currentFrame.Position;
            transform.rotation = currentFrame.Rotation; 
        }
    }
}

#region Helpers
/// <summary>
/// A struct for storing time, rotation and position.
/// </summary>
public struct Keyframe
{
    #region Properties
    public float Time { get; private set; }
    public Vector3 Position { get; private set; }
    public Quaternion Rotation { get; private set; }
    #endregion

    #region Constructors
    public Keyframe (float time, Vector3 position, Quaternion rotation)
    {
        Time = time;
        Position = position;
        Rotation = rotation;
    }
    #endregion
}
#endregion