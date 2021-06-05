using UnityEngine;
using System.Collections;

public class E_10_Bullet : MonoBehaviour
{

	[SerializeField]
	float moveSpeed = 5f;

	[SerializeField]
	float frequency = 20f;

	[SerializeField]
	float magnitude = 0.5f;

	Vector3 pos;

	float spawnTime;

	// Use this for initialization
	void Start()
	{
		pos = transform.position;
		spawnTime = Time.time;

	}

	// Update is called once per frame
	void Update()
	{

		MoveDown();
	}

	void MoveDown()
	{
		float lifeTime = Time.time - spawnTime;
		pos += (-transform.up) * Time.deltaTime * moveSpeed;
		transform.position = pos + transform.right * Mathf.Sin(lifeTime * frequency) * magnitude;

	}

}