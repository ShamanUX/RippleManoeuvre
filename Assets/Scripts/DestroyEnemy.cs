
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) > 20)
        {
            GameObject.Destroy(transform.parent.gameObject);
        }
    }
}
