using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Attach this script to a GameObject with a trigger accociated to it.
 * When anything enters the trigger, all objects in the Objects to Move array will begin moving along the offset for a time equal to duration
 */
public class MoveObstical : MonoBehaviour
{
    [SerializeField] Vector3 offset = Vector3.zero;
    [SerializeField] float duration = 1;

    [SerializeField] GameObject[] ObjectsToMove;

    private void OnTriggerEnter(Collider other)
    {
        foreach(GameObject g in ObjectsToMove)
            StartCoroutine(MoveObject(g));
    }

    // Code inspired from gamedevbeginner https://gamedevbeginner.com/how-to-move-objects-in-unity/
    IEnumerator MoveObject(GameObject objectToMove)
    {
        Vector3 initPosition = objectToMove.transform.position;
        Vector3 targetPosition = initPosition + offset;
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            objectToMove.transform.position = Vector3.Lerp(initPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        objectToMove.transform.position = targetPosition;
    }
}
