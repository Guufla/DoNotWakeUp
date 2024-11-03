using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sandwich : MonoBehaviour
{
    public Rigidbody2D plate;
    public float maxSpeed = 10f;
    public float speed = 2;
    public float spawnDeviation = 200f;
    public GameObject[] sandwiches;
    public float spawnRate = 5f;
    public Transform spawnPoint;
    public GameObject celebrate;
    [Tooltip("Time to display celebration")]
    public float winDelay = 2;

    List<GameObject> spawnedThings = new List<GameObject>();
    float horizInput;
    float timer = 0;
    int spawnItem = 0;

    bool won = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = spawnRate - 2;
        celebrate.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Moves plate
        horizInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            plate.AddForce(new Vector2(horizInput * speed * plate.mass, 0));
            plate.velocity = Vector2.ClampMagnitude(plate.velocity, maxSpeed);
        }

        timer += Time.deltaTime;
        if (timer > spawnRate && spawnItem < sandwiches.Length)
        {
            timer = 0;
            float rand = Random.Range(-spawnDeviation, spawnDeviation);
            spawnPoint.position = new Vector3(transform.position.x + rand, spawnPoint.position.y); // Randomizes X value of position
            GameObject spawned = Instantiate(sandwiches[spawnItem], spawnPoint.position, Quaternion.identity, transform);
            spawnedThings.Add(spawned);
            spawnItem++;
        }
        else if(timer > spawnRate && spawnItem >= sandwiches.Length && !won)
        {
            won = true;
            celebrate.SetActive(true);
            StartCoroutine(DelayRemove());
        }
    }

    IEnumerator DelayRemove()
    {
        yield return new WaitForSeconds(winDelay);
        transform.parent.parent.GetComponent<Minigame>().CompleteMinigame();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(GameObject obj in spawnedThings)
        {
            Destroy(obj);
        }
        spawnedThings.Clear();
        timer = 0;
        spawnItem = 0;
    }
}
