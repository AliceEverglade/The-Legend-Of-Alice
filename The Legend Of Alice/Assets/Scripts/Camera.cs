using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera : MonoBehaviour
{
    private CinemachineFreeLook freelook;
    private CinemachineFreeLook.Orbit[] originalOrbits;

    [Range(0.2f, 1f)]
    public float zoomPercent;

    public void Awake()
    {
        freelook = GetComponent<CinemachineFreeLook>();
        originalOrbits = new CinemachineFreeLook.Orbit[freelook.m_Orbits.Length];
        for (int i = 0; i < freelook.m_Orbits.Length; i++)
        {
            originalOrbits[i].m_Height = freelook.m_Orbits[i].m_Height;
            originalOrbits[i].m_Radius = freelook.m_Orbits[i].m_Radius;
        }
        zoomPercent = 1f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < freelook.m_Orbits.Length; i++)
        {
            freelook.m_Orbits[i].m_Height = originalOrbits[i].m_Height * zoomPercent;
            freelook.m_Orbits[i].m_Radius = originalOrbits[i].m_Radius * zoomPercent;
        }
    }
}
