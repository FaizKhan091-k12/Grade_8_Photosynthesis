using System;
using UnityEngine;

public class CloudRunner : MonoBehaviour
{
    [SerializeField] private float speed = 10f;


    private void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject,2f);
    }
}
