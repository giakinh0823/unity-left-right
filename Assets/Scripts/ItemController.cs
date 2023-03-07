using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Camera mainCamera; // Tham chiếu đến camera trong Scene
    public PlayerController playerController;

    private void Start()
    {
        mainCamera = Camera.main;
        playerController = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        // Chuyển đổi vị trí của đối tượng sang không gian viewport
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(gameObject.transform.position);

        // Kiểm tra nếu vị trí của đối tượng nằm ngoài giới hạn của viewport thì hủy đối tượng
        if (viewportPosition.x < 0f || viewportPosition.x > 1f || viewportPosition.y < 0f || viewportPosition.y > 1f || viewportPosition.z < 0f)
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("Triangle"))
            {
                playerController.health--;
            }
        }
    }
}
