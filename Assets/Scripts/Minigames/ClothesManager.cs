using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesManager : MonoBehaviour
{
    public Transform spriteParent;
    public int clothesNum = 5;
    public GameObject[] clothesObj;
    public GameObject celebrate;
    [Tooltip("Time to display celebration")]
    public float winDelay = 2;

    List<GameObject> clothes = new List<GameObject>();
    int removedClothes = 0;
    bool won = false;

    // Start is called before the first frame update
    void Start()
    {
        celebrate.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        for (int i = 0; i < clothesNum; i++)
        {
            int rand = Random.Range(0, clothesObj.Length);
            GameObject newClothes = Instantiate(clothesObj[rand], spriteParent.position, Quaternion.identity, spriteParent);
        }
    }

    private void Update()
    {
        if (removedClothes == clothesNum && !won)
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<Clothes>() && !clothes.Contains(collision.transform.gameObject))
        {
            clothes.Add(collision.transform.gameObject);
            removedClothes++;
            Debug.Log(removedClothes);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<Clothes>() && clothes.Contains(collision.transform.gameObject))
        {
            removedClothes--;
            clothes.Remove(collision.transform.gameObject);
        }
    }
}
