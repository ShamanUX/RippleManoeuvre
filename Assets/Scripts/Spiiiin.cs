using UnityEngine;

public class Spiiiin : MonoBehaviour
{
    public Transform target;
	public float orbitSpeed;

    void Update()
    {
        if (target == null) return;
		
		Vector3 northWest = Vector3.down + Vector3.left;
		
        transform.RotateAround(target.position, northWest, orbitSpeed * Time.deltaTime);
		
    }
}
