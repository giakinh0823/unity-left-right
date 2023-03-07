using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject rect; // Danh sách các đối tượng sẽ được spawn
    public GameObject triangle; // Danh sách các đối tượng sẽ được spawn

    private float spawnRate = 5f; // Tần suất spawn đối tượng

    private float speed = 1f; // Tốc độ rơi của đối tượng

    private Camera mainCamera; // Camera chính của game
    private float lastSpawnTime; // Thời điểm spawn đối tượng cuối cùng

    [SerializeField]
    private PlayerController playerController;

    void Start()
    {
        mainCamera = Camera.main;
        lastSpawnTime = Time.time;
    }

    void Update()
    {
        // Tính khoảng thời gian giữa các lần spawn
        float timeSinceLastSpawn = Time.time - lastSpawnTime;
        float rate = (float)((spawnRate - (playerController.health * 0.1f) >= 1) ? spawnRate - (playerController.health * 0.1f) : 1);

        // Nếu đã đến lúc spawn mới
        if (timeSinceLastSpawn >= rate)
        {
            float cameraHeight = 2f * mainCamera.orthographicSize; // Tính chiều cao của camera
            float cameraWidth = cameraHeight * mainCamera.aspect; // Tính chiều rộng của camera
            float yMax = mainCamera.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y; // Lấy toạ độ y của camera trên cùng
            float xMin = mainCamera.transform.position.x - cameraWidth / 2f; // Tính toạ độ x tối thiểu của đoạn thẳng
            float xMax = mainCamera.transform.position.x + cameraWidth / 2f; // Tính toạ độ x tối đa của đoạn thẳng
            float randomX = Random.Range(xMin, xMax); // Tạo một số ngẫu nhiên trong khoảng giá trị từ xMin đến xMax
            Vector3 spawnPosition = new Vector3(randomX, yMax, 0f); // Tạo một điểm có tọa độ ngẫu nhiên trên đoạn thẳng

            // Chọn ngẫu nhiên một đối tượng từ danh sách
            float randomValue = Random.Range(0f, 1f);
            GameObject objectToSpawn = null;
            if (randomValue <= 0.8f)
            {
                objectToSpawn = rect;
            }
            else
            {
                objectToSpawn = triangle;
            }

            // Spawn đối tượng
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

            // Đặt tốc độ rơi của đối tượng
            Rigidbody2D objectRigidbody = spawnedObject.GetComponent<Rigidbody2D>();
            if (objectRigidbody != null)
            {
                objectRigidbody.velocity = new Vector2(0f, -(speed + (playerController.health * 0.1f)));
            }

            // Cập nhật thời điểm spawn cuối cùng
            lastSpawnTime = Time.time;
        }
    }
}
