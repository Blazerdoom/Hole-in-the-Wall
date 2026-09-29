using UnityEngine;

public class ObsticleMove : MonoBehaviour
{
    public int speed = 3;
    public GameObject wallDisappearEffect;
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;
        if(transform.position.z < -6)
        {
            if (wallDisappearEffect != null)
            {
                Instantiate(wallDisappearEffect, transform.position + Vector3.up * 5f, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        
    }
}
