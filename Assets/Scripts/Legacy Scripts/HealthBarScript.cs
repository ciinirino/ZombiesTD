using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{

    public Slider healthBarSlider;
    public int maxHealth;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        healthBarSlider = GetComponent<Slider>();
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = maxHealth;
        currentHealth = maxHealth;
    }

    public void setHealth()
    {
        healthBarSlider.value = currentHealth;
    }

}
