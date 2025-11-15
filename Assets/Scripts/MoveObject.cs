using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float MoveSpeedX;
    public float MoveSpeedY;
    public float MoveSpeedZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(MoveSpeedX * Time.deltaTime, MoveSpeedY * Time.deltaTime, MoveSpeedZ * Time.deltaTime, Space.World);
    }
}
