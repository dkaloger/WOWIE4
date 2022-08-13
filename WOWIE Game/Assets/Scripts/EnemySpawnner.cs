using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float sizeOfSpawnPoint;
    public bool showGuideLines;
    public float t;
    public float wavecooldown;
    public int wavesizeMin, wavesizeMax;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        t+=Time.deltaTime;
      
        if (t>wavecooldown)
        {
            GameObject.FindGameObjectWithTag("Wavealert").GetComponent<Animation>().Play();
            for (int i = 0; i < Random.Range(wavesizeMin,wavesizeMax); i++)
            {
                SpawEnemy();
            }
            t = 0;
            

        }
    }

    private void SpawEnemy()
    {
        Vector3 randomPos = Random.insideUnitSphere * sizeOfSpawnPoint;
        randomPos += transform.position;
        randomPos.y = 0f;

        Vector3 direction = randomPos - transform.position;
        direction.Normalize();

        float dotProduct = Vector3.Dot(transform.forward, direction);
        float dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);

        randomPos.x = Mathf.Cos(dotProductAngle) * sizeOfSpawnPoint + transform.position.x;
        randomPos.y = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * sizeOfSpawnPoint + transform.position.y;
        randomPos.z = transform.position.z;

        GameObject go = Instantiate(enemy, randomPos, Quaternion.identity);
        go.transform.position = randomPos;
    }

    private void OnDrawGizmos()
    {
        if (!showGuideLines) return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, sizeOfSpawnPoint);
    }
}

