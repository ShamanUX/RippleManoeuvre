using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    private bool initialForceSet = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {

        int forceMagnitude = 2;
        if (other.CompareTag("Player") && !initialForceSet)
        {
            initialForceSet = true;
            // Calculate direction FROM transform TO other object
            Vector3 forceDirection = (other.transform.position - transform.position).normalized;

            // Apply force to the other object
            Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
            if (otherRigidbody != null)
            {
                otherRigidbody.AddForce(new Vector3(forceDirection.x, forceDirection.y, 0) * forceMagnitude, ForceMode.Impulse);
            }
        }
    }
}
