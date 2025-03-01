using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [Header("Coleccionables")]
    [SerializeField] private int balas = 10;
    [SerializeField] private int monedas = 0;

    [Header("Propiedades")]
    [SerializeField] private float salud = 100f;
    private float jumpForce = 5f;
    private int saltosMaximos = 2;
    private int saltos;
    [SerializeField] private float speed;
    [SerializeField] private Transform lastCheckPoint;

    private Rigidbody rb;
    private UIEstadisticas estadisticas;

    [Header("Dash")]
    [SerializeField] private float dash = 20f;
    [SerializeField] private float duracionDash = 0.2f;
    [SerializeField] private float EsperaDash = 1f;
    private bool puedeDash = true;
    private bool estaDasheando = false;


    private float hInput, vInput;

    void Start()
    {
        estadisticas = FindObjectOfType<UIEstadisticas>();
        rb = GetComponent<Rigidbody>();
        saltos = saltosMaximos;
    }

    void Update()
    {
        // Movimiento horizontal
        hInput = -Input.GetAxisRaw("Horizontal");
        vInput = -Input.GetAxisRaw("Vertical");

        if (vInput > 0)
            vInput = 0; 


        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && saltos > 0)
        {
            rb.AddForce(new Vector3(0, 1, 0) * jumpForce, ForceMode.Impulse);
            saltos--;
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && puedeDash)
        {

                StartCoroutine(Dash()); // Iniciar la corrutina correctamente
        
        }


        if (salud <= 0) {
            transform.position = lastCheckPoint.position;
            transform.eulerAngles = Vector3.zero;
            salud = 100f;
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(hInput, 0, vInput).normalized * speed, ForceMode.Force);

    }

    private IEnumerator Dash()
    {
        puedeDash = false;
        estaDasheando = true;

        rb.AddForce(new Vector3(hInput, 0, vInput).normalized * dash, ForceMode.Impulse);

        yield return new WaitForSeconds(duracionDash);

        estaDasheando = false;

        yield return new WaitForSeconds(EsperaDash);
        puedeDash = true;
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
            estadisticas.TakeDamage(trampaScript.Trampa);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Monedita"))
        if (other.gameObject.TryGetComponent(out moneda monedaScript))
        {
            int coin = ((int)monedaScript.Monedas);
            monedas += coin;
            estadisticas.TakeMoneda(monedas);
            Destroy(other.gameObject);
        }

        if (other.gameObject.TryGetComponent(out bala balasScript))
        {
            int bullet = ((int)balasScript.Bala);
            balas += bullet;
            Debug.Log(balas);
            if (balas  > 10) {
                balas = 10;
                Debug.Log(balas);

            }

            estadisticas.TakeMunicion(balas);
            Destroy(other.gameObject);
        }
    }
}