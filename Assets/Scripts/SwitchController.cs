using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SwitchController : MonoBehaviour
{
    public Collider ball;
    public Material offMaterial;
    public Material onMaterial;

    private bool isOn;
    private Renderer renderer;

    public ScoreManager scoreManager;
    public float score;

    private void Start()
    {
        renderer = GetComponent<Renderer>();

        Set(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == ball)
        {
            Set(!isOn);
            scoreManager.AddScore(score);

        }
    }

    private void Set(bool active)
    {
        isOn = active;
        if (isOn == true)
        {
            renderer.material = onMaterial;
        }
        else
        {
            renderer.material = offMaterial;
        }
    }
}
