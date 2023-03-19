using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    private Rigidbody2D rb;
    private Transform groundCheck;
    private LayerMask groundLayer;

    //ejecutar comando constantemente
    void Update()
    {
        //guardar movimiento WASD en la variable horizontal
        horizontal = Input.GetAxisRaw("Horizontal");
        
        //si el boton "saltar" se ha presionado y el personaje esta sobre suelo, generar impulso en el eje Y con la fuerza de la variable JumpingPower (se define mas tarde la variable "IsGrounded")
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        //si el boton "saltar" se ha presionado y la velocidad vertical sea inferior a 0, multiplicar la velocidad del eje Y por 0.5
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        //ejecutar Flip
        Flip();
    }

    //ejecutar comando a tiempo real
    private void FixedUpdate()
    {
        //velocidad del eje X = variable horizontal * variable speed
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    //variable booleana "IsGrounded" que verifica si el personaje esta tocando el suelo
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    /*variable Flip, si la variable "IsFacingRight" es verdadera i la variable "horizontal" es menor a 0, O si la variable "isFacingRight" es falsa i la variable "horizontal" es mayor a 0, 
    que invierta la variable "isFacingRight" y invierta la imagen del personaje*/
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}