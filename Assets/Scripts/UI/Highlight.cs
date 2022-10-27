using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*SUMMARY (Updated oct 26 2022)
 * 
 * code and idea from https://www.sunnyvalleystudio.com/blog/unity-3d-selection-highlight-using-emission
 * 
 * this is a script that gets renderers assigned in inspector,
 * gets all the materials attached to those renderers,
 * and when ToggleHighlight(true)
 *      turns on emission with a color for those materials
 * when ToggleHighlight(false)
 *      turns off the immision for those materials
 */

public class Highlight : MonoBehaviour
{
    [SerializeField]
    private List<Renderer> renderers;

    [SerializeField]
    private Color color = Color.white;

    //list to chache all the materials of this object
    private List<Material> materials;

    //get the materials form each renderer
    private void Awake()
    {
        materials = new List<Material>();
        foreach(var renderer in renderers)
        {
            //gets every material in each renderer
            materials.AddRange(new List<Material>(renderer.materials));
        }
    }

    public void ToggleHighlight(bool val)
    {
        if(val)
        {
            foreach(var material in materials)
            {
                //enable emission
                material.EnableKeyword("_EMISSION");
                //then set emission color
                material.SetColor("_EmissionColor", color);
            }
        }
        else
        {
            foreach (var material in materials)
            {
                //just disable imission
                material.DisableKeyword("_EMISSION");
            }
        }
    }
}
