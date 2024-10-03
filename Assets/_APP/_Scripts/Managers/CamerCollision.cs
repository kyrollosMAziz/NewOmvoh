using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CamerCollision : MonoBehaviour
{
    [SerializeField] Camera cam;
    int oldMask;
    bool isColliding = false;
    private void Start()
    {
        cam = GetComponent<Camera>();
        oldMask = cam.cullingMask;
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collided");
        if (collision.gameObject.tag == "wall")
        {
            Debug.Log("Collided 2");

            int layer2 = LayerMask.NameToLayer("Nothing");
            cam.cullingMask = (1 << layer2);
            cam.clearFlags = CameraClearFlags.SolidColor;

            // Set the background color to black
            cam.backgroundColor = Color.black;
            isColliding = true;

            //StartCoroutine(PlaySFX(3f));

        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Debug.Log("Collided 2");

            // int layer2 = LayerMask.NameToLayer("Nothing");
            cam.cullingMask = oldMask;
            cam.clearFlags = CameraClearFlags.Skybox;
            isColliding = false;
            StopAllCoroutines();
            //StartCoroutine(PlaySFX(0f));

            // Set the background color to black
            // cam.backgroundColor = Color.black;
        }
    }

    //IEnumerator PlaySFX(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    if (isColliding)
    //    {
    //        AudioManagerMain.instance.PlaySFX("TakeStepBack");
    //    }
    //    else
    //    {
    //        AudioManagerMain.instance.StopSound("TakeStepBack");
    //    }
    //}

}
