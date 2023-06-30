using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    private IEnumerator Start()
    {
        float   elapsedTime   = 0f;
        Vector3 startRotation = Vector3.zero;
        Vector3 endRotation   = new Vector3(0f, 0f, -360f);
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime / 2f;
            Vector3 lerpedRotation = Vector3.Lerp(startRotation, endRotation, elapsedTime);
            transform.rotation = Quaternion.Euler(lerpedRotation);

            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene("Gameplay");
    }
}