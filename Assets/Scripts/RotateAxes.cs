using UnityEngine;

public class RotateAxes : MonoBehaviour
{
    public float RotateSpeedX;
    public float RotateSpeedY;
    public float RotateSpeedZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotateSpeedX * Time.deltaTime, RotateSpeedY * Time.deltaTime, RotateSpeedZ * Time.deltaTime);
    }
}
