using UnityEngine;
using UnityEngine.Rendering.Universal;

public class spawner : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRate = 10f;
    private float timer = 0f;
    public float heightOffset = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            spawnpipe();
            timer = 0f;
        }
        
    }
    void spawnpipe() {
        float lowestpoint = transform.position.y - heightOffset;
        float highestpoint = transform.position.y + heightOffset;
        Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestpoint,highestpoint),0), transform.rotation); 
    }
}
