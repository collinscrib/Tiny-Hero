using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balista : MonoBehaviour {

    public float thrust = 5f;
    public Rigidbody boltRigidBody;

    private bool fired = false;
    private Animator animBalista;

    private void Start()
    {
        animBalista = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9) // if player
        {
            FireBalista();
        }
    }

    public void FireBalista()
    {
        if(fired == false)
        {
            animBalista.SetTrigger("Fire");
            Vector3 forwardUp = new Vector3(0f, 0.0f, -100f);
            boltRigidBody.isKinematic = false;
            boltRigidBody.AddForce(forwardUp * thrust * Time.deltaTime);
            animBalista.SetBool("Fired", true);
            fired = true;
        }
    }

    public void ReloadBalista()
    {
        if(fired)
        {
            boltRigidBody.isKinematic = true;
            boltRigidBody.transform.localPosition = Vector3.zero;
            boltRigidBody.transform.rotation = Quaternion.Euler(0, -90, 0);
            animBalista.SetBool("Fired", false);
            fired = false;
        }
    }
}