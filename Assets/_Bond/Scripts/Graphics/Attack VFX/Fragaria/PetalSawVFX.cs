using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PetalSawVFX : MonoBehaviour
{
    public GameObject rootObject;

    public Vector3 minRotation;
    public Vector3 maxRotation;

    public List<float> slashTimes;
    public GameObject slashPrefab;

    // Start is called before the first frame update
    void Start()
    {
        foreach(float delay in slashTimes)
        {
            StartCoroutine(PlaySlash(delay));
        }
        Invoke("Complete", slashTimes.Max() + 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Complete()
    {
        Destroy(rootObject);
    }

    IEnumerator PlaySlash(float delay)
    {
        yield return new WaitForSeconds(delay);
        Transform slash = Instantiate(slashPrefab, transform).transform;

        //randomize orientation here
        slash.rotation = Quaternion.Euler(Random.Range(minRotation.x, maxRotation.x), Random.Range(minRotation.y, maxRotation.y), Random.Range(minRotation.z, maxRotation.z)) * slash.rotation;
    }
}
