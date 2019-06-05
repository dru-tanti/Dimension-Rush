using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    public float speed;
    public float torque;

    private Rigidbody2D _pickaxeRB;

    private void Awake()
	{
		// We're looking for the Rigidbody2D component
		// on this object, and taking reference.
		_pickaxeRB = GetComponent<Rigidbody2D>();
	}

    private void Start()
	{
		_pickaxeRB.velocity = transform.TransformDirection(Vector2.up) * speed;
		
		_pickaxeRB.AddTorque(torque, ForceMode2D.Impulse);
	}
}
