using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class Camera : MonoBehaviour
{
    private CinemachineFreeLook freelook;
    private CinemachineFreeLook.Orbit[] originalOrbits;
    private InputMaster InputMaster;
    private InputAction LookAround;
    private InputAction Zoom;
    public Vector2 zoomInput;

    public float zoomSensitivity = 1;
    public float maxZoom;
    public float zoomPercent = 5;

    public void Awake()
    {
        InputMaster = new InputMaster();

        freelook = GetComponent<CinemachineFreeLook>();
        originalOrbits = new CinemachineFreeLook.Orbit[freelook.m_Orbits.Length];
        for (int i = 0; i < freelook.m_Orbits.Length; i++)
        {
            originalOrbits[i].m_Height = freelook.m_Orbits[i].m_Height;
            originalOrbits[i].m_Radius = freelook.m_Orbits[i].m_Radius;
        }
        zoomPercent = 1f;
    }

    private void OnEnable()
    {
        Zoom = InputMaster.Camera.Zoom;
        Zoom.Enable();
        LookAround = InputMaster.Camera.lookaround;
        LookAround.Enable();
    }
    private void OnDisable()
    {
        Zoom.Disable();
        LookAround.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        maxZoom = 3;
    }

    // Update is called once per frame
    void Update()
    {
        ZoomHandler();
        

        for (int i = 0; i < freelook.m_Orbits.Length; i++)
        {
            freelook.m_Orbits[i].m_Height = originalOrbits[i].m_Height * zoomPercent;
            freelook.m_Orbits[i].m_Radius = originalOrbits[i].m_Radius * zoomPercent;
        }
    }
    void ZoomHandler()
    {
        zoomInput = Zoom.ReadValue<Vector2>();
        zoomPercent -= zoomInput.y * Time.deltaTime * zoomSensitivity;
        if (zoomPercent < 0.2f)
        {
            zoomPercent = 0.2f;
        }
        if (zoomPercent > maxZoom)
        {
            zoomPercent = maxZoom;
        }
        
    }
}
