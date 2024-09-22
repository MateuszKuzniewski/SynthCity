using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Stats")] 
    [Space(2)]
    public int health;
    public int maxHealth;
    public float movementSpeed;
    public float meleeDamage;
    public float rangeDamage;
    public float projectileSpeed;
    public float projectileDeleteTime;
    public bool isDead;

    private bool isAttackOnCooldown;

    [Header("Objects")]
    [Space(2)]
    public GameObject targetMarker;
    public GameObject projectile;
    public GameObject GunPoint;
    private ItemBox itemBoxScript;
    private Enemy enemyScript;
    private NavMeshAgent navMesh;
    private GameObject player;
    private Animator animator;

    [Header("Types")]
    [Space(2)]
    public EnemyStates enemyState;
    public EnemyType enemyType;
    public EnemyAlignment enemyAlignment;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        animator = GetComponent<Animator>();
        enemyScript = GetComponent<Enemy>();
        navMesh = GetComponent<NavMeshAgent>();
        itemBoxScript = GetComponent<ItemBox>();
        isDead = false;
        itemBoxScript.enabled = false;
        targetMarker.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyState == EnemyStates.COMBAT)
            Approach(player, 4);

        if (enemyAlignment == EnemyAlignment.HOSTILE && DistanceFromTarget(player) < 6)
            enemyState = EnemyStates.COMBAT;

    }

    public void OnMouseOver()
    {
        ObjectNamesUI.ChangeTextColour(Color.red);
        ObjectNamesUI.DisplayObjectName(gameObject.name);
    
    }

    public void OnMouseExit()
    {
        ObjectNamesUI.HideObjectName();
    }

    public void TakeDamage(int damage)
    {
        if (damage < health)
        {
            health -= damage;
            UI.instance.UpdateEnemyHealthBar(gameObject);
        }
        else
        {
            health -= damage;
            if(enemyState != EnemyStates.DEAD)
            {
                OnDeath();
                UI.instance.EnemyPortait.SetActive(false);
            }
        }

    }

    public void OnDeath()
    {
        isDead = true;
        itemBoxScript.enabled = true;
        enemyScript.enabled = false;
        targetMarker.gameObject.SetActive(false);
        enemyState = EnemyStates.DEAD;
        movementSpeed = 0;
        navMesh.isStopped = true;
        navMesh.speed = 0;
        animator.SetBool("Death", true);
        name = name + " (DEAD)";
        
    }

    public void Approach(GameObject target, float maxDistance)
    {
        if (DistanceFromTarget(target) > maxDistance && enemyState != EnemyStates.DEAD)
        {
            navMesh.isStopped = false;
            navMesh.destination = target.transform.position;
            navMesh.speed = 6;
            animator.SetFloat("Speed", 1);
        }
        else
        {
            navMesh.isStopped = true;
            navMesh.speed = 0;
            StartCoroutine(Attack());
            animator.SetFloat("Speed", 0);
        }
    }

    public IEnumerator Attack()
    {
        if(isAttackOnCooldown == false)
        {
            isAttackOnCooldown = true;
            animator.SetTrigger("Attack");
            GameObject pClone = Instantiate(projectile, GunPoint.transform.position, GunPoint.transform.rotation);
            pClone.transform.rotation = GunPoint.transform.rotation;

            pClone.GetComponent<Projectile>().speed = projectileSpeed;
            pClone.GetComponent<Projectile>().time = projectileDeleteTime;
            pClone.GetComponent<Projectile>().damage = meleeDamage;
            yield return new WaitForSeconds(2);
            isAttackOnCooldown = false;
        }
    }


    public float DistanceFromTarget(GameObject target)
    {
        float dist = Vector3.Distance(gameObject.transform.position, target.transform.position);
        return dist;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile") && enemyState != EnemyStates.DEAD)
        {
            enemyState = EnemyStates.COMBAT;
            InputManager.instance.SelectTarget(gameObject);
            float p_damage = collision.gameObject.GetComponent<Projectile>().damage;
            TakeDamage((int)p_damage);
        }
    }

    public enum EnemyStates
    {
        COMBAT, DEAD, MOVING, IDLE
    }

    public enum EnemyType
    {
        MELEE, RANGED
    }
    public enum EnemyAlignment
    {
        NEUTRAL, HOSTILE
    }
}