using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class KIllPunishment : MonoBehaviour
{
    [SerializeField] float delayToDestroy;
    public static Action<GameObject> GetBonkedBitch;
    Rigidbody2D rb;
    AudioSource AudioSource;
    bool playOnce;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AudioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerClimbing player))
        {
            Destroy(gameObject, delayToDestroy);
            GetBonkedBitch?.Invoke(collision.gameObject);
            rb.AddTorque(Random.Range(-90,90));
            rb.AddForce(new Vector2(Random.Range(-1,1) *Random.Range(0,25),Random.Range(-1,1)) * Random.Range(0,25));
            if(!playOnce)
            {
                AudioSource.Play();
                playOnce = true;
            }
        }
    }
}