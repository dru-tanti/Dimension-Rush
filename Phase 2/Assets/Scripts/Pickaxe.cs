using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    public float speed;
    public float torque;
	public float lifetime;

    private Rigidbody2D _pickaxeRB;

    private void Awake()
	{
		_pickaxeRB = GetComponent<Rigidbody2D>();
	}

    private void Start()
	{
		_pickaxeRB.velocity = transform.TransformDirection(Vector2.right) * speed;
		
		_pickaxeRB.AddTorque(torque, ForceMode2D.Impulse);

		Destroy(gameObject, lifetime);
	}
}
