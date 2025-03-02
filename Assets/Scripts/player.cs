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
    [SerializeField] private float jumpForce = 1000f;
    [SerializeField] private int saltosMaximos = 2;
    [SerializeField] private int saltos;
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


    [Header("Disparo")]
    [SerializeField] private GameObject prefabBala; 
    [SerializeField] private Transform spawnBala;
    [SerializeField] private float velocidadBala = 10f;

    private float hInput, vInput;
    public Transform arma;
    void Start()
    {
        estadisticas = FindObjectOfType<UIEstadisticas>();
        rb = GetComponent<Rigidbody>();
        saltos = saltosMaximos;
    }

    private void Disparar()
    {
        if (balas <= 0) return;

        Vector3 posicionInicial = spawnBala.position - spawnBala.forward * 1.5f;
        GameObject balaInstanciada = Instantiate(prefabBala, posicionInicial, spawnBala.rotation);

        Rigidbody rbBala = balaInstanciada.GetComponent<Rigidbody>();


        rbBala.useGravity = true;
        rbBala.velocity = Quaternion.Euler(-20f, 0f, 0f) * -spawnBala.forward * velocidadBala;


        balas--;
        estadisticas.TakeMunicion(balas);
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

                StartCoroutine(Dash()); // Corrutina
        
        }

        // Disparar
        if (Input.GetMouseButtonDown(0) && balas > 0)
        {
            Disparar();
        }

        if (salud <= 0|| transform.position.y < -5) {
            Debug.Log("Jugador ha muerto. Reapareciendo en: " + lastCheckPoint.position);


            if (lastCheckPoint != null)
            {
                transform.position = lastCheckPoint.position;
            }
            else
            {
                transform.position = Vector3.zero;
            }

            transform.rotation = Quaternion.identity;


            Camera.main.transform.rotation = Quaternion.identity;
            
            salud = 100f;
            rb.velocity = Vector3.zero;
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(hInput, 0, vInput).normalized * speed, ForceMode.Force);

    }
    public void SetCheckpoint(Transform checkpoint)
    {
        lastCheckPoint = checkpoint;
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
        if (collision.gameObject.CompareTag("Suelo") || collision.gameObject.CompareTag("Trampa") || collision.gameObject.CompareTag("Lava"))
        {
            saltos = saltosMaximos; 
        }
        if (collision.gameObject.CompareTag("Trampa") || collision.gameObject.CompareTag("Lava"))
        {
            collision.gameObject.TryGetComponent(out trampa trampaScript);
            salud -=trampaScript.Trampa;
            estadisticas.TakeDamage(trampaScript.Trampa);
        }
        if (collision.gameObject.CompareTag("TrampaLava"))
        {
            Destroy(collision.gameObject);
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

            }

            estadisticas.TakeMunicion(balas);
            Destroy(other.gameObject);
        }
    }
}