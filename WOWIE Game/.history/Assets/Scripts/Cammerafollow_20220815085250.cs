using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cammerafollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    public Vector3 Offset;
    public Bounds Bounds;
    public Vector3 min,max;
    Vector3 shake;
    public Vector3 shaking;
    Vector2 camerasize;
    void FixedUpdate()
    {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(Offset);

        // Smoothly move the camera towards that target position
        
        camerasize.x =  + Camera.main.orthographicSize * Screen.width / Screen.height;
        camerasize.y =  + Camera.main.orthographicSize;
        Vector3 tmppos;
       

           // transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        
        tmppos.x = Mathf.Clamp(Vector3.SmoothDamp(transform.position, targetPosition+ shake, ref velocity, smoothTime).x , min.x+camerasize.x,max.x-camerasize.x);
        // tmppos.y = Mathf.Clamp(Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime).y, (Bounds.center.y - Bounds.extents.y / 2)-camerasize.y, (Bounds.center.y - Bounds.extents.y / 2)+ camerasize.y);
        tmppos.y = Mathf.Clamp(Vector3.SmoothDamp(transform.position, targetPosition + shake, ref velocity, smoothTime).y, min.y + camerasize.y, max.y - camerasize.y);
        tmppos.z = Offset.z;
       // print(tmppos);
        transform.position = tmppos;
        shake = Vector3.zero;

    }
    public void ShakeCamera(float mag)
    {          
                shake = new Vector3(Random.Range(shaking.x, shaking.y) * shaking.z* mag, Random.Range(shaking.x, shaking.y) * shaking.z* mag, 0);
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 origPos = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            //float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(origPos.x + x,origPos.y,origPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = origPos;
        Debug.Log("Done shaking");
        StartCoroutine(Drop());

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(Bounds.center, Bounds.extents);
    }
}
