using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARTouch : MonoBehaviour
{


    public GameObject doorPopup;
    public GameObject bubblePrefab;

    private List<GameObject> bubbles = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed primary button.");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log("hit");
                Debug.Log(hit.transform.name + " : " + hit.transform.tag);

                if (hit.transform.tag == "Door")
                {
                    Vector3 pos = hit.point;
                    pos.z += 0.25f;
                    pos.y += 0.25f;
                    Quaternion rotation = Quaternion.Euler(0f, 180f, 0f);
                    Instantiate(doorPopup, pos, rotation);
                }

                if (hit.transform.tag == "Button")
                {
                    CreateBubblesAroundPlayer();
                }

                if (hit.transform.tag == "Info")
                {
                    Destroy(hit.transform.gameObject);
                }

            }

        }
    }

    private void CreateBubblesAroundPlayer()
    {
        Vector3 originPosition = transform.position; // Get XR origin position

        for (int i = 0; i < 20; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * 3f; // Adjust 3f to control the radius
            randomDirection.y = 0f; // Keep the bubbles at the same height as the XR origin

            Vector3 bubblePos = originPosition + randomDirection;

            // Make sure the bubble is not too close to the XR origin
            if (!Physics.CheckSphere(bubblePos, 0.5f)) // Adjust 0.5f to control the minimum distance
            {
                GameObject bubble = Instantiate(bubblePrefab, bubblePos, Quaternion.identity);
                bubbles.Add(bubble);
            }
        }
    }
}