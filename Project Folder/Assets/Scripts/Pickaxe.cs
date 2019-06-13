using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    public float speed;
    public float torque;
	public float lifetime;
	private Transform player;

    private Rigidbody2D _pickaxeRB;

    private void Awake()
	{
		AudioManager.current.Play("AxeThrow");
		_pickaxeRB = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

    private void Start()
	{
		Vector3 dir = (player.position - transform.position).normalized;
		_pickaxeRB.velocity = transform.TransformDirection(dir) * speed;
		
		_pickaxeRB.AddTorque(torque, ForceMode2D.Impulse);

		Destroy(gameObject, lifetime);
	}
}
