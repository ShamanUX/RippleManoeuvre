using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class TouchEventListener : MonoBehaviour
{

    public GameObject ripple;
    public GameObject windUpParticles;
    private GameObject windUpParticlesInstance;
    private float holdTime;

    private Vector3 ripplePosition;
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
            ripplePosition = new Vector3(Worldpos.x, Worldpos.y, 0);
            holdTime = 0;
            windUpParticlesInstance = Instantiate(windUpParticles);
            windUpParticlesInstance.transform.position = ripplePosition;
        }
        else if (Mouse.current.leftButton.isPressed)
        {
            holdTime += Time.deltaTime;
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            Destroy(windUpParticlesInstance);
            GameObject newRipple = GameObject.Instantiate(ripple);
            newRipple.transform.position = ripplePosition;
            newRipple.GetComponent<ControlColliderAndParticles>().SetStrength(holdTime);
        }
    }
}
