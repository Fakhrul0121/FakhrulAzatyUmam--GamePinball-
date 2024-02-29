using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperController : MonoBehaviour
{
    /// <summary>
    /// Variable for color
    /// </summary>
    public Color color;
    private Renderer renderer;

    public Collider ball;
    public float multiplier;

    private Animator animator;

    // tambahkan audio manager untuk mengakses fungsi pada audio managernya
    public AudioManager audioManager;

    // tambahkan vfx manager untuk mengakses fungsi pada audio managernya
    public VFXManager VFXManager;

    // untuk mengakses score manager
    public ScoreManager scoreManager;
    public float score;

    private void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<Renderer>();
        renderer.material.color = color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == ball)
        {
            // ambil rigidbody nya lalu kali kecepatannya sebanyak multiplier agar bisa memantul lebih cepat
            Rigidbody ballRig = ball.GetComponent<Rigidbody>();
            ballRig.velocity *= multiplier;
            animator.SetTrigger("hit");

            // kita jalankan SFX saat tabrakan dengan bola pada posisi tabrakannya
            audioManager.PlaySFX(collision.transform.position);

            // kita jalankan VFX saat tabrakan dengan bola pada posisi tabrakannya
            VFXManager.PlayVFX(collision.transform.position);

            //tambah score saat menabrak bumper
            scoreManager.AddScore(score);
        }
    }
}
