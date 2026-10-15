using UnityEngine;

public class ObsticleMove : MonoBehaviour
{
    public int speed = 3;
    public GameObject wallDisappearEffect;
    Renderer[] blocks;
    float slow = 1f;
    float flashTime = 0f;
    float flashLength = 0.25f;

    void Start()
    {
        blocks = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        slow = Mathf.MoveTowards(slow, 1f, 2f * Time.deltaTime);
        transform.position += Vector3.back * speed * slow * Time.deltaTime;
        flashing();
        if(transform.position.z < -6)
        {
            if (wallDisappearEffect != null)
            {
                Instantiate(wallDisappearEffect, transform.position + Vector3.up * 5f, Quaternion.identity);
            }
            GameManager.instance.Play("pass", Random.Range(0.9f, 1.1f), 0.4f);
            Destroy(gameObject);
        }

    }

    public void Hit()
    {
        slow = 0.2f;
        flashTime = flashLength;
    }

    void flashing()
    {
        if (flashTime > 0f)
        {
            flashTime -= Time.deltaTime;
            foreach (Renderer block in blocks)
            {
                block.material.SetColor("_EmissionColor", Color.white * Mathf.Max(flashTime, 0f) / flashLength);
            }
        }
    }
}
