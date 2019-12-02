using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // The target we are following
    public Transform target;

    // The distance in the x-z plane to the target
    public float distance = 10.0f;

    // the height we want the camera to be above the target
    public float height = 5.0f;

    // Tweak vars
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    void FixedUpdate()
    {
        // Early out if we don't have a target
        if (!target)
            return;

        Vector3 followpos = new Vector3(0.0f, height, -distance);
        Quaternion lookrotation = Quaternion.identity;

        lookrotation.eulerAngles = new Vector3(30.0f, 0.0f, 0.0f);

        Matrix4x4 m1 = Matrix4x4.TRS(target.position, target.rotation, Vector3.one);
        Matrix4x4 m2 = Matrix4x4.TRS(followpos, lookrotation, Vector3.one);
        Matrix4x4 combined = m1 * m2;

        // Get the position and rotation
        Vector3 position = combined.GetColumn(3);

        Quaternion rotation = 
            Quaternion.LookRotation(combined.GetColumn(2),
                                    combined.GetColumn(1)
            );

        Quaternion wantedRotation = rotation;
        Quaternion currentRotation = transform.rotation;

        Vector3 wantedPosition = position;
        Vector3 currentPosition = transform.position;

        currentRotation = /* interpolate and normalize */
            Quaternion.Lerp(currentRotation,
                            wantedRotation, 
                            rotationDamping * Time.deltaTime
            );

        currentPosition = /* idem: position */
            Vector3.Lerp(currentPosition, 
                         wantedPosition, 
                         heightDamping * Time.deltaTime
            );

        transform.localRotation = currentRotation;
        transform.localPosition = currentPosition;
    }
}
