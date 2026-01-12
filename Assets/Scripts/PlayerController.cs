using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class PlayerController : MonoBehaviour
{
    public float thrustForce=1f;
    public float maxSpeed = 5f;
    private float elapsedTime= 0f;
    private float score = 0f;
    public float scoreMultiplier = 10f;

    public GameObject boosterFlame;    
    private Rigidbody2D rb;

    public UIDocument uiDocument;
    private Label scoreText;
    private Button restartButton;

    public GameObject explosionEffect;

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText= uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;
        restartButton.clicked += ReloadScene;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        scoreText.text = "Score: " + score;
        if (Mouse.current.leftButton.isPressed)
        {

            // lấy vị trí chuột trong thế giới 
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            // tính toán hướng từ người chơi đến vị trí bấm chuột và chuẩn hóa nó 
            Vector2 direction = (mousePos - transform.position).normalized;
            // xoay người chơi về hướng di chuyển
            transform.up = direction;
            // thêm vào lực để người chơi di chuyển về hướng chuột bấm 
            rb.AddForce(direction * thrustForce);
            // giới hạn tốc độ tối đa 
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            boosterFlame.SetActive(true);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            boosterFlame.SetActive(false);
        }
         
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // khi va chạm với bất kỳ vật thể nào, hủy đối tượng người chơi
        Destroy(gameObject);
        Instantiate(explosionEffect, transform.position, transform.rotation);
        restartButton.style.display = DisplayStyle.Flex;
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
