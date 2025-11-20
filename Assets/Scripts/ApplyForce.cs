using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ChangeColor))]
public class ApplyForce : MonoBehaviour
{
    public float constantForceMultiplier = 1.5f;
    public float impactForceMultiplier = 1.5f;

    private bool isAffectingPlayer;

    private bool initialForceSet = false;

    private float rippleEffectiveRadius;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        rippleEffectiveRadius = GetComponentInParent<ControlColliderAndParticles>().rippleEffectiveRadius;

    }

    public void setIsAffectingPlayer(bool state)
    {
        isAffectingPlayer = state;
    }

    public bool getIsAffectingPlayer()
    {
        return isAffectingPlayer;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 forceDirection = (other.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(other.transform.position, transform.position);
            float parentRadius = transform.parent.localScale.x / 2;

            //Debug.Log("Distance: " + distance + " " + "parentRadius " + parentRadius);
            // 
            if (other.TryGetComponent<Rigidbody>(out var otherRigidbody))
            {
                if (parentRadius - distance < rippleEffectiveRadius + .5)
                {
                    otherRigidbody.AddForce(new Vector3(forceDirection.x, forceDirection.y, 0) * constantForceMultiplier, ForceMode.Force);
                    other.GetComponent<ChangeColor>().ChangeToRed();
                    isAffectingPlayer = true;
                }
                else
                {
                    other.GetComponent<ChangeColor>().RevertToOriginalColor();
                    isAffectingPlayer = false;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ChangeColor>().RevertToOriginalColor();
        }
    }

    void OnDestroy()
    {
        if (isAffectingPlayer)
        {
            GetComponent<ChangeColor>().RevertToOriginalColor();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !initialForceSet)
        {
            initialForceSet = true;
            // Calculate direction FROM transform TO other object
            Vector3 forceDirection = (other.transform.position - transform.position).normalized;

            // Apply force to the other object
            Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
            if (otherRigidbody != null)
            {
                otherRigidbody.AddForce(new Vector3(forceDirection.x, forceDirection.y, 0) * impactForceMultiplier, ForceMode.Impulse);
            }
        }
    }
}
