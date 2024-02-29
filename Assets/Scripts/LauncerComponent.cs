using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncerComponent : MonoBehaviour
{
    public Collider ball; // referensi ke bola
    public KeyCode input; // tombol input untuk aktivasi launch
    public float force; // besar gaya yang diberikan saat launch

    // untuk set berapa lama waktu yang harus dicapai hingga maksimal force
    public float maxTimeHold;
    // untuk set berapa besar maksimal force yang bisa didapat (ini menggantikan force)
    public float maxForce;

    // state pada launcher
    private bool isHold;

    private void Start()
    {
        // di set false state nya saat baru mulai
        isHold = false;
    }

    // hanya dapat membaca input saat bersentuhan dengan bola saja
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider == ball)
        {
            ReadInput(ball);
        }
    }

    // baca input
    private void ReadInput(Collider collider)
    {
        if (Input.GetKey(input) && !isHold)
        {
            // dorong bola ke atas dengan menggunakan gaya dorong dngn besaran tertentu
            StartCoroutine(StartHold(collider));
        }
    }

    private IEnumerator StartHold(Collider collider)
    {
        // di set true dulu
        isHold = true;

        float force = 0.0f;
        float timeHold = 0.0f;

        while (Input.GetKey(input))
        {
            // isi perhitungan force disini
            force = Mathf.Lerp(0, maxForce, timeHold / maxTimeHold);

            // tunggu step berikutnya dan naikan timer 
            // agar mendapat nilai force yang lebih besar dari sebelumnya
            yield return new WaitForEndOfFrame();
            timeHold += Time.deltaTime;
        }

        // kalau tombol dilepas, maka proses hold selesai
        collider.GetComponent<Rigidbody>().AddForce(Vector3.forward * force);
        isHold = false;
    }
}
