using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunParticle : MonoBehaviour {

	private ParticleSystem pSys;
	private ParticleSystem.Particle[] particles;
	private Particle[] particleProperty;
    private float riseTime;

	// Use this for initialization
	void Start () 
	{
		particles = new ParticleSystem.Particle[1000];  
		particleProperty = new Particle[1000];  

		pSys = this.GetComponent<ParticleSystem>();  

		pSys.maxParticles = 1000;
		pSys.startSpeed = 0; 

		pSys.startSize = 0.05f;           
		pSys.loop = false;    
		 
		pSys.Emit(1000);  

		pSys.GetParticles(particles);

		for (int i = 0; i < 1000; i++) 
		{
			float midRadius = 1.0f;  
			float minRate = Random.Range(1.0f, midRadius / 2.0f);  
			float maxRate = Random.Range(midRadius / 4.0f, 1.0f);  
			float radius = Random.Range(1.0f * minRate, 1.0f * maxRate);
			float angle = Random.Range(0.0f, 360.0f);
			float time = Random.Range(0.0f, 360.0f);

			particleProperty[i] = new Particle (radius, angle, time);
			particles[i].position = new Vector3 (particleProperty[i].radius * Mathf.Cos (particleProperty[i].angle / 180 * Mathf.PI), 
																						particleProperty[i].radius * Mathf.Sin (45 / 180 * Mathf.PI),
																						particleProperty[i].radius * Mathf.Sin (particleProperty [i].angle / 180 * Mathf.PI));
		}
		pSys.SetParticles(particles, 1000);

        riseTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (gameObject.transform.parent.parent.GetComponent<Item>().dead)
        {
            int type = gameObject.transform.parent.parent.GetComponent<Item>().type;
            for (int i = 0; i < 1000; i++)
            {
                particleProperty[i].angle -= 10f;
                particleProperty[i].angle = particleProperty[i].angle % 360.0f;

                particles[i].position = new Vector3(particleProperty[i].radius * Mathf.Cos(particleProperty[i].angle / 180 * Mathf.PI),
                                                                                            particleProperty[i].radius * Mathf.Sin(45 / 180 * Mathf.PI) + riseTime,
                                                                                            particleProperty[i].radius * Mathf.Sin(particleProperty[i].angle / 180 * Mathf.PI));
                switch (type)
                {
                    case 0:
                        particles[i].color = Color.green;
                        break;
                    case 1:
                        particles[i].color = Color.red;
                        break;
                    case 2:
                        particles[i].color = Color.blue;
                        break;
                    default:
                        particles[i].color = Color.white;
                        break;
                }
            }
            riseTime += Time.deltaTime;
            if (riseTime >= 1.0f)
            {
                Destroy(gameObject.transform.parent.parent.gameObject);
            }
        }
        else
        {
            for (int i = 0; i < 1000; i++)
            {
                particleProperty[i].angle -= 10f;
                particleProperty[i].angle = particleProperty[i].angle % 360.0f;

                particles[i].position = new Vector3(particleProperty[i].radius * Mathf.Cos(particleProperty[i].angle / 180 * Mathf.PI),
                                                                                            particleProperty[i].radius * Mathf.Sin(45 / 180 * Mathf.PI),
                                                                                            particleProperty[i].radius * Mathf.Sin(particleProperty[i].angle / 180 * Mathf.PI));          
            }
        }

		pSys.SetParticles(particles, 1000);
	}
}
