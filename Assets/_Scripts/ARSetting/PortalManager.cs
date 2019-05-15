using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PortalManager : MonoBehaviour {
    public GameObject m_MainCamera;

    public GameObject Sponza;

    private Material[] SponzaMaterials;

    private void Start()
    {
        SponzaMaterials = Sponza.GetComponent<Renderer>().sharedMaterials;
    }

    // Update is called once per frame
    void OnTriggerStay (Collider collider) {
        Vector3 camPositionInPortalSpace = transform.InverseTransformPoint(m_MainCamera.transform.position);

        if (camPositionInPortalSpace.y < 1.0f)
        {
            // Disable sponza shader
            for (int i = 0; i < SponzaMaterials.Length; ++i)
            {
                SponzaMaterials[i].SetInt("_StencilComp", (int)CompareFunction.Always);
            }
        }
        else
        {
            // Enable sponza shader
            for (int i = 0; i < SponzaMaterials.Length; ++i)
            {
                SponzaMaterials[i].SetInt("_StencilComp", (int)CompareFunction.Equal);
            }
        }
	}
}
