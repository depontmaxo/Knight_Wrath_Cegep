using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Ennemy : MonoBehaviour
{

   

    private NavMeshAgent agent;

    [SerializeField] private int degats = 10;
    [SerializeField] private int vie = 100;
    [SerializeField] private float attackSpeed = 1.5f;
    [SerializeField] private float sightRange = 10.0f;
    [SerializeField] private float attackRange = 2.5f;
    [SerializeField] private float attackRadius = 0.5f;
    [SerializeField] private AudioClip mortSon;
    [SerializeField] private GameObject _effect;
    private Rigidbody _rb;

    private Transform player;
    private Player _Player;
    public Transform attackPoint;
    public LayerMask playerLayers;
    private Animator _anim;
    public Slider slider;

    private float vieF = 0;
    private float vieMax = 0;

    bool destinationSet;
    bool walk;
    bool run;
    bool idle;
    bool attack;

    Vector3 spawnPlace;
    Vector3 destination;

    private void Start()
    { 
        vieMax = vie;
        slider.value = CalculateHealth();
    }
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        _Player = FindObjectOfType<Player>();
        agent = GetComponent<NavMeshAgent>();
        spawnPlace = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        
        if (Vector3.Distance(transform.position, player.position) <= sightRange && Vector3.Distance(transform.position, player.position) > attackRange && attack == false) Follow();
        if (Vector3.Distance(transform.position, player.position) <= attackRange && Vector3.Distance(transform.position, player.position) <= sightRange && attack == false) Attack();
        if (Vector3.Distance(transform.position, player.position ) > sightRange && attack == false) Return();
        Mort();

        slider.value = CalculateHealth();
    }


    // l'ennemie suit le joueur
    private void Follow()
    {
        //anim
        attack = false;
        _anim.SetBool("Run", true);

        destination = player.position;
        agent.destination = destination;
    }

    //L'ennemie Attack le joueur
    private void Attack()
    {
        /*
         * start un coroutine pour lattaque avec le bool attack et faire arreter la fonction actuelle le temps de l'attaque
         */
        destinationSet = false;
        attack = true;
        _anim.SetBool("At", true);
        StartCoroutine(Beathim());
        //turn
        Vector3 directionRegard = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(directionRegard);
        transform.rotation = rotation;

        
    }
    IEnumerator Beathim()
    {
        
        yield return new WaitForSeconds(0.75f * attackSpeed);
        //detect player
        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint.position, attackRadius);

        foreach (Collider c in hitPlayers) 
        {
            if (c.transform.tag == "Player")
            {
                c.GetComponent<Player>().Dommage(degats);
            }
            
        }
        yield return new WaitForSeconds(0.25f * attackSpeed);
        attack = false;
        _anim.SetBool("At", false);
        _rb.velocity = new Vector3(0, 0, 0);
    }

    //L'ennemie 
    private void Return()
    {

        //anim
        _anim.SetBool("Run", true);

        if (!destinationSet)
        {
            agent.destination = spawnPlace;
        }
        if (Vector3.Distance(transform.position, spawnPlace) < 2.5)
        {
            _anim.SetBool("Run", false);
        }


    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    
    public void PrendreDegats(int degats)
    {
        vie -= degats;
    }

    private void Mort()
    {
        if (vie <= 0)
        {
            _Player.addHealth();
            AudioSource.PlayClipAtPoint(mortSon, this.transform.position, 1.3f);
            Destroy(this.gameObject);
            Instantiate(_effect, (transform.position), transform.rotation);
        }
    }
    public bool gotHit()
    { 
        if (vie < vieMax)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
 
    //----------------------------------------health bar------------------------------------------

    public float CalculateHealth()
    {
        vieF = vie;
        return vieF/vieMax;
    }
}
