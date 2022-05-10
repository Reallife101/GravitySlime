using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    [SerializeField] GameObject bbeg;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] List<GameObject> fists;
    [SerializeField] GameObject camera;
    [SerializeField] GameObject cObject;

    private IEnumerator coroutine;
    private void OnTriggerEnter(Collider other)
    {
        bbeg.SetActive(true);
        StartCoroutine(Lerp(3f));
        coroutine = fight(1.5f);
        StartCoroutine(coroutine);
    }

    public void stopC()
    {
        if (coroutine == null)
            return;
        StopCoroutine(coroutine);

        foreach (GameObject fist in fists)
        {
            fist.GetComponent<movePlayer>().displacement = new Vector3(30f, fist.transform.position.y, 0);
        }

        camera.GetComponent<movePlayer>().displacement = new Vector3(10f, 0f, -12f);
        cObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        bbeg.SetActive(false);
    }

    IEnumerator Lerp(float lerpDuration)
    {
        Color oldColor = sr.color;
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            sr.color = new Color(oldColor.r, oldColor.g, oldColor.b, Mathf.Lerp(oldColor.a, 1, timeElapsed / lerpDuration));

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        sr.color = new Color(oldColor.r, oldColor.g, oldColor.b, 1);
    }

    IEnumerator fadeout(float lerpDuration)
    {
        Color oldColor = sr.color;
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            sr.color = new Color(oldColor.r, oldColor.g, oldColor.b, Mathf.Lerp(1, 0, timeElapsed / lerpDuration));

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        sr.color = new Color(oldColor.r, oldColor.g, oldColor.b, 0);
        bbeg.SetActive(false);
    }

    IEnumerator fight(float lerpDuration)
    {
        yield return new WaitForSeconds(3f);

        //start fight
        int numAttacks = 0;
        float newX = 6f;

        while (numAttacks <=6)
        {
            movePlayer go = fists[Random.Range(0, fists.Count)].GetComponent<movePlayer>();

            Vector3 oldPosition = go.displacement;
            float timeElapsed = 0;

            while (timeElapsed < lerpDuration)
            {
                go.displacement = new Vector3(Mathf.Lerp(oldPosition.x, newX, timeElapsed / lerpDuration), oldPosition.y, oldPosition.z);

                timeElapsed += Time.deltaTime;
                yield return null;
            }
            go.displacement = new Vector3(newX, oldPosition.y, oldPosition.z);

            timeElapsed = 0;
            yield return new WaitForSeconds(1f);

            while (timeElapsed < lerpDuration)
            {
                go.displacement = new Vector3(Mathf.Lerp(newX, oldPosition.x, timeElapsed / lerpDuration), oldPosition.y, oldPosition.z);

                timeElapsed += Time.deltaTime;
                yield return null;
            }
            go.displacement = new Vector3(oldPosition.x, oldPosition.y, oldPosition.z);
            numAttacks += 1;
        }

        StartCoroutine(fadeout(3f));
    }
}
