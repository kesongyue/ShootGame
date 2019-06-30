using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    private Slider lifeSlider;
    private Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        scene = Scene.getInstance();
        lifeSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeSlider.value > scene.playerBlood()) {
            lifeSlider.value -= 1;
        }
    }
}
