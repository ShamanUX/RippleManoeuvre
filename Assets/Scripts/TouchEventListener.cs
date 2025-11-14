using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class TouchEventListener : MonoBehaviour
{

    public GameObject ripple;

    Vector3 mousePosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (
             Mouse.current.leftButton.wasPressedThisFrame
           )
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 Worldpos = Camera.main.ScreenToWorldPoint(mousePos);
            Debug.Log(Worldpos);
            GameObject newRipple = GameObject.Instantiate(ripple);
            newRipple.transform.position = Worldpos;

        }
    }

    void OnMouseDown()
    {
        Debug.Log("MouseDown");
    }
}
