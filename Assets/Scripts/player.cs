using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float salud = 100f;
    private float jumpForce = 5f;
    private int saltosMaximos = 2;
    private int monedas = 0;
    private int saltos;
    [SerializeField] private float speed;

    private Rigidbody rb;

    [Header("Distancia")]
    [SerializeField] private float distance;
    [SerializeField] private LayerMask whatIsInteractuable;
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float radioInteraccion;


    private float hInput, vInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        saltos = saltosMaximos;
    }

    void Update()
    {
        // Movimiento horizontal
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");


        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && saltos > 0)
        {
            rb.AddForce(new Vector3(0, 1, 0) * jumpForce, ForceMode.Impulse);
            saltos--;
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(hInput, 0, vInput).normalized * speed, ForceMode.Force);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            saltos = saltosMaximos; 
        }
        if (collision.gameObject.CompareTag("Trampa"))
        {
            collision.gameObject.TryGetComponent(out trampa trampaScript);
            salud -=trampaScript.Trampa;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Monedita"))
        if (other.gameObject.TryGetComponent(out moneda monedaScript))
        {
            monedas += ((int)monedaScript.Monedas);
            Destroy(other.gameObject);
        }
    }
}