using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;



public class Player : MonoBehaviour
{



    [SerializeField] private Camera playerCamera;
    [SerializeField] private InputActionAsset _actionAsset = default;
    [SerializeField] private GameObject _epee;
    [SerializeField] private GameObject _shield = default;
    [SerializeField] public GameObject gameOverMenuUI;
    [SerializeField] private int _viesJoueur = 10;
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float rotationSpeed = 420;
    [SerializeField] private AudioClip sword;
    [SerializeField] private AudioClip shield;
    [SerializeField] private AudioClip douleur;




    public HealthBar healthBar;

    private Vector3 _direction;
    private Rigidbody _rb;
    private Animator _anim;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction AttackAction;
    private InputAction ShieldAction;

    private bool attaque = false;
    private bool shieldOn = false;
    private bool deplacement = false;
    public bool rune1 = false;
    public bool rune2 = false;
    public bool rune3 = false;
    public bool rune4 = false;
    public bool estVivant = true;
    
    private int bestScene = 0;
    private int vieMax = 100;
    private int shieldSave = 20;
    
    private float _canAttack = -1.0f;
    private float _cadenceAttack = 0.5f;
    
    string highScene = "highScene";
    //--------------------------------------------------------------------------------------------------------------------------------------------




    private void Awake()
    {
        vieMax = _viesJoueur;
        _rb = this.GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();//------------------------------------------------------------------------------------------------------------------

        setIntScene(highScene);

    }





    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(0f, 0f, 0f); // Déplace le joueur à sa position initiale
        healthBar.setMaxHealth(_viesJoueur);



        //Active et désactive le déplacement du joueur avec l'action Move
        moveAction = _actionAsset.FindAction("Move");
        moveAction.canceled += MoveAction_canceled;
        moveAction.Enable();


        //Active et désactive le saut du joueur avec l'action Jump
        jumpAction = _actionAsset.FindAction("Jump");
        jumpAction.performed += jumpAction_performed;
        jumpAction.Enable();


        //Active et désactive l'attaque du joueur avec l'action Attack
        AttackAction = _actionAsset.FindAction("Attack");
        AttackAction.performed += AttackAction_performed;
        AttackAction.Enable();


        //Active et désactive le bouclier du joueur avec l'action Shield
        ShieldAction = _actionAsset.FindAction("Shield");
        ShieldAction.performed += ShieldAction_performed;
        ShieldAction.canceled += ShieldAction_canceled;
        ShieldAction.Enable();



    }




    void FixedUpdate()
    {

        if (isGrounded() == false)
        {
            // _rb.AddForce(Vector2.down * 9.8f, ForceMode.Force);
        }


        //Vérifie si le bouclier est utilisé avant de se déplacer
        if (attaque == false && shieldOn == false) Movement();

        //Charge la scène finale si les runes sont activées
        if (rune1 == true && rune2 == true && rune3 == true && rune4 == true && SceneManager.GetActiveScene().buildIndex == 5)
        {
            endScene();
            SceneManager.LoadScene(6);
        }

    }

    //Déplacement du joueur
    private void MoveAction_canceled(InputAction.CallbackContext obj)
    {
        _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
    }


    //Saut du joueur
    private void jumpAction_performed(InputAction.CallbackContext obj)
    {
        if (isGrounded() == true && attaque == false && shieldOn == false)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
            _anim.SetBool("Jump", true);//---------------------------------------------------------------
            StartCoroutine(SautFin());
        }

    }

    //Coroutine pour la fin du saut
    IEnumerator SautFin()
    {
        yield return new WaitForSeconds(0.90f);
        _anim.SetBool("Jump", false);
    }



    
    //Rotation du joueur selon le mouvement
    private void Movement()
    {
        Vector2 direction2D = moveAction.ReadValue<Vector2>();
        _rb.transform.Translate(new Vector3(direction2D.x, 0, direction2D.y) * Time.deltaTime * _speed, relativeTo: null);
        Vector3 movementDirection = new Vector3(direction2D.x, 0, direction2D.y);
        movementDirection.Normalize();
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);



            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);



            _anim.SetBool("Run", true);//---------------------------------------------------------------------------------------------------------------------------
        }
        else
        {
            _anim.SetBool("Run", false);//---------------------------------------------------------------------------------------------------------------------------
        }

    }



    //Fonction de l'attaque du joueur
    private void AttackAction_performed(InputAction.CallbackContext obj)
    {
        if (Time.time > _canAttack && shieldOn == false && attaque == false && isGrounded() == true)
        {
            attaque = true;
            StartCoroutine(ArretEpee());
            _canAttack = Time.time + _cadenceAttack;
        }



    }


    //Coroutine pour l'action d'attaquer
    IEnumerator ArretEpee()
    {

        _anim.SetBool("LeftClick", true);
        yield return new WaitForSeconds(0.55f);
        _epee.SetActive(true);
        AudioSource.PlayClipAtPoint(sword, this.transform.position,1.3f);
        yield return new WaitForSeconds(0.25f);
        _epee.SetActive(false);
        attaque = false;
        _anim.SetBool("LeftClick", false);
    }


    //Action du bouclier
    private void ShieldAction_performed(InputAction.CallbackContext obj)
    {
        shieldOn = true;
        _shield.SetActive(true);
        _anim.SetBool("RightClick", true);
    }


    //Fin de l'action du bouclier
    private void ShieldAction_canceled(InputAction.CallbackContext obj)
    {
        shieldOn = false;
        _shield.SetActive(false);
        _anim.SetBool("RightClick", false);
    }



    //Vérifie si le joueur est au sol
    private bool isGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * .25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.5f))
        {
            return true;
        }
        else
        {
            return false;
        }

    }



    public void endScene()
    {
        jumpAction.performed -= jumpAction_performed;
        moveAction.canceled -= MoveAction_canceled;
        AttackAction.performed -= AttackAction_performed;
        ShieldAction.performed -= ShieldAction_performed;
        ShieldAction.canceled -= ShieldAction_canceled;

    }

    public void Dommage(int degat)
    {
        if (!_shield.activeSelf)
        {
            AudioSource.PlayClipAtPoint(douleur, this.transform.position, 1.3f);
            _viesJoueur -= degat;
            healthBar.SetHealth(_viesJoueur);
        }
        else
        {
            AudioSource.PlayClipAtPoint(shield, this.transform.position, 1.3f);
            if (degat > shieldSave)
            {
                _viesJoueur -= (degat - shieldSave);
                healthBar.SetHealth(_viesJoueur);
                _anim.SetTrigger("Trigger");
            }
        }



        if (_viesJoueur < 1)
        {
            estVivant = false;
            endScene();
            gameOverMenuUI.SetActive(true);
        }
    }

    public void addHealth()
    {
        if (_viesJoueur < (vieMax - 10))
        {
            _viesJoueur += 10;
        }
        else
        {
            _viesJoueur = vieMax;
        }
        healthBar.SetHealth(_viesJoueur);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Death")
        {
            Dommage(1000);
        }
    }

    private void setIntScene(string name)
    {

        bestScene = Getint(name);

        int scene = SceneManager.GetActiveScene().buildIndex;
        if (bestScene < scene && bestScene != 7)
        {
            bestScene = scene;
            PlayerPrefs.SetInt(name, bestScene);
        }
        if (scene == 6)
        {
            bestScene = 1;
            PlayerPrefs.SetInt(name, bestScene);
        }

        
    }

    public int Getint(string KeyName)
    {
        return PlayerPrefs.GetInt(KeyName);
    }

}
