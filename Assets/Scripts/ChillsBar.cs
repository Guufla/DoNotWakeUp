using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChillsBar : MonoBehaviour
{
    [SerializeField] float chillsRate = 1f;

    Slider chillsSlider;


    // Start is called before the first frame update
    void Start()
    {
        chillsSlider = GetComponent<Slider>();
    }


    void FixedUpdate()
    {
        chillsSlider.value += chillsRate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
