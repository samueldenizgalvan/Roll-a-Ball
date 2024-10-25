using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public TMP_Text countText; // Texto para mostrar el conteo de objetos recolectados
    public TMP_Text winText; // Texto que muestra "You Win" al ganar
    public float speed = 10.0f; // Velocidad de movimiento
    public float jumpForce = 5.0f; // Fuerza del salto
    private Rigidbody rb; // Referencia al Rigidbody del jugador
    public int count; // Contador de objetos recolectados
    private float movementX; // Movimiento en el eje X
    private float movementY; // Movimiento en el eje Y
    private bool isGrounded; // Verificación si el jugador está en el suelo

    void Start()
    {
        count = 0;
        SetCountText(); // Actualiza el texto de conteo inicial
        rb = GetComponent<Rigidbody>(); // Asigna el Rigidbody
        winText.gameObject.SetActive(false); // Oculta el texto de victoria al inicio
    }

    // Método llamado cuando se detecta movimiento
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Método llamado cuando se detecta el salto
    void OnJump(InputValue value)
    {
        if (isGrounded) // Solo salta si está en el suelo
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Aplica fuerza hacia arriba para saltar
            isGrounded = false; // Evita que el jugador salte de nuevo en el aire
        }
    }

    // Método llamado en cada FixedUpdate para aplicar el movimiento
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed); // Aplica fuerza de movimiento
    }

    // Detección de la recolección de objetos
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) // Verifica si colisiona con un objeto de tipo "PickUp"
        {
            other.gameObject.SetActive(false); // Desactiva el objeto recolectado
            count++; // Incrementa el contador
            SetCountText(); // Actualiza el texto del contador
        }
    }

    // Detecta cuando el jugador está tocando el suelo
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")) // Verifica si el jugador está tocando un objeto etiquetado como "Ground"
        {
            isGrounded = true; // Confirma que el jugador está en el suelo
        }
    }

    // Detecta cuando el jugador deja de tocar el suelo
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")) // Verifica si el jugador dejó de tocar el suelo
        {
            isGrounded = false; // Indica que el jugador ya no está en el suelo
        }
    }
    private void SetCountText()
    {
    countText.text = "Count: " + count.ToString();
    if (count >= 13) // Condición de victoria
    {
        winText.gameObject.SetActive(true); // Muestra el texto de "You Win"
        FindObjectOfType<GameManager>().ShowWinMenu(); // Llama al GameManager para mostrar el menú
    }
    }
}
