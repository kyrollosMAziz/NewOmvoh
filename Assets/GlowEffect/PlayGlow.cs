using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGlow : MonoBehaviour
{
    [SerializeField] Material glowMaterial;
    private string floatPropertyName = "_OverallStrength";

    [SerializeField] private float _overallStrength = 1;
    // Start is called before the first frame update

    public void Glow()
    {
        glowMaterial.SetFloat(floatPropertyName, _overallStrength);
    }

    public void ResetMaterial()
    {
        glowMaterial.SetFloat(floatPropertyName, 0);
    }

    void Start()
    {
        ResetMaterial();
        //Glow();
    }

    private void OnDestroy()
    {
        ResetMaterial();
    }

    // Update is called once per frame
    void Update()
    {
    }
}