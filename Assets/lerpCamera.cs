using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerpCamera : MonoBehaviour
{
    [SerializeField] GameObject camera;
    [SerializeField] GameObject cObject;
    [SerializeField] Vector3 displacement;
    [SerializeField] Vector3 rotation;
    [SerializeField] float time;

    movePlayer mp;
    Transform t;
    private void OnTriggerEnter(Collider other)
    {
        mp = camera.GetComponent<movePlayer>();
        t= camera.GetComponent<Transform>();
        StartCoroutine(Lerp(time));
    }

    IEnumerator Lerp(float lerpDuration)
    {
        Vector3 oldDisplacement = mp.displacement;
        Vector3 oldRotation = t.rotation.eulerAngles;
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            mp.displacement = new Vector3(Mathf.Lerp(oldDisplacement.x, displacement.x, timeElapsed / lerpDuration), 
                Mathf.Lerp(oldDisplacement.y, displacement.y, timeElapsed / lerpDuration),
                Mathf.Lerp(oldDisplacement.z, displacement.z, timeElapsed / lerpDuration));

            t.rotation = Quaternion.Euler(new Vector3(Mathf.Lerp(oldRotation.x, rotation.x, timeElapsed / lerpDuration),
                Mathf.Lerp(oldRotation.y, rotation.y, timeElapsed / lerpDuration),
                Mathf.Lerp(oldRotation.z, rotation.z, timeElapsed / lerpDuration)));

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        mp.displacement = displacement;
        cObject.transform.rotation = Quaternion.Euler(rotation);
    }
}
