using UnityEngine;

public class Spiiiin : MonoBehaviour
{
    public Transform target;
	public float orbitSpeed;

    void Update()
    {
        if (target == null) return;
		
        transform.RotateAround(target.position, Vector3.up, orbitSpeed * Time.deltaTime);
		
    }
}
