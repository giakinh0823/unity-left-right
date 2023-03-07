using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    private float minX, maxX;

    private float horizontal;
    private float speed = 20f; // Tốc độ di chuyển của đối tượng
    [SerializeField]
    public Text score; // Tham chiếu đến đối tượng Canvas Text
    public float health = 2;

    [SerializeField]
    public GameObject gameOver;
    [SerializeField]
    public GameObject spawnObject;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();


        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;
        Vector2 camPos = cam.transform.position;

        // Tính toán giới hạn di chuyển
        minX = camPos.x - camWidth / 2f;
        maxX = camPos.x + camWidth / 2f;

        if (score != null)
        {
            score.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        // Lấy input di chuyển từ bàn phím hoặc thiết bị di động
        horizontal = Input.GetAxis("Horizontal");

        // Tính toán vị trí mới của đối tượng
        Vector2 newPos = transform.position + new Vector3(horizontal * speed * Time.deltaTime, 0, 0);

        // Giới hạn vị trí mới trong giới hạn di chuyển
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);

        // Cập nhật vị trí của đối tượng
        transform.position = newPos;

        score.text = "Score: " + health.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rect"))
        {
            health -= 1;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Triangle"))
        {
            health += 1;
            Destroy(collision.gameObject);
        }

        if(health <= 0)
        {
            Destroy(gameObject);
            score.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if(gameOver != null)
        {
            gameOver.gameObject.SetActive(true);
        }
        if(spawnObject != null)
        {
            spawnObject.SetActive(false);
        }
    }
}
