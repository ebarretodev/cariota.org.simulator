using UnityEngine;

public class CarLights : MonoBehaviour {
    private void GetInput(){
        m_input_break = Input.GetKey(KeyCode.Space);
        m_input_vertical = Input.GetAxis("Vertical");
    }

    private void BreakLight(){
        tailLights.materials[26].EnableKeyword("_EMISSION");
        if(m_input_break){
            tailLights.materials[26].SetColor("_EmissionColor", Color.red);
        } else {
            tailLights.materials[26].SetColor("_EmissionColor", Color.black);
        }
    }

    private void BackLights(){
        tailLights.materials[25].EnableKeyword("_EMISSION");
        if(m_input_vertical < 0){
            tailLights.materials[25].SetColor("_EmissionColor", Color.white);
        } else {
            tailLights.materials[25].SetColor("_EmissionColor", Color.black);
        }
    }

    private void FixedUpdate()
    {
        GetInput();
        BreakLight();
        BackLights();
    }
    public Renderer tailLights;
    private bool m_input_break;
    private float m_input_vertical;


}