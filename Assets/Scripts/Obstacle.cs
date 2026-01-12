using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public float minSize = 0.5f;
    public float maxSize = 2.0f;
    public float minSpeed= 50f;
    public float maxSpeed= 150f;
    public float maxSpinSpeed = 10f;
    public GameObject bounceEffectPrefab;
    private Rigidbody2D rb;

    void Start()
    {
        float randomSize = Random.Range(minSize, maxSize);
        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize;
        float randomTorque = Random.Range(-maxSpinSpeed, maxSpinSpeed);

        transform.localScale = new Vector3(randomSize, randomSize, 1);


        rb = GetComponent<Rigidbody2D>();
        Vector2 randomDirection = Random.insideUnitCircle;
        rb.AddForce(randomDirection * randomSpeed);
        rb.AddTorque(randomTorque);

    }

    void Update()

    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 contactPoint = collision.GetContact(0).point;
        GameObject bounceEffect = Instantiate(bounceEffectPrefab, contactPoint, Quaternion.identity);

        Destroy(bounceEffect, 0.5f);
    }

}
