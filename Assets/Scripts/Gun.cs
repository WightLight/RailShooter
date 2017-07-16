using UnityEngine;

public class Gun : MonoBehaviour
{

	public float damage = 10f;
	public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

	public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public AudioSource shotSFX;

    public float fireCooldown = 0f;

	// Update is called once per frame
	void Update ()
	{

	}

	public void Shoot()
	{
        if (Time.time >= fireCooldown)
        {
            fireCooldown = Time.time + 1 / fireRate;

            muzzleFlash.Play();
            shotSFX.Play();

            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log("Camera pointed at: " + hit.collider.ToString());

                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }

                GameObject impactObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactObject, 2);
            }
        }
	}
}
