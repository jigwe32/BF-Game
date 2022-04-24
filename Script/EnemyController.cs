using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private HealthComponent health;

    private Transform player;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float range = 7.5f;
    [SerializeField] private float attackRange = 0.5f;

    public bool targettingPlayer = false;
    private bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        health = GetComponent <HealthComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameController.GamePaused)
        {
          if(Vector3.Distance(transform.position, player.position) <= range)
          {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            transform.LookAt(player);
            targettingPlayer = true;
          }
          else
            targettingPlayer = false;

          if(targettingPlayer && Vector3.Distance(transform.position, player.position) <= attackRange && !isAttacking)
          {
            isAttacking = true;
            StartCoroutine(Attack());
          }
        }
    }
    private IEnumerator Attack()
    {
      yield return new WaitForSeconds(Random.Range(0.5f, 2f));
      player.GetComponent<HealthComponent>().UpdateHealth(-5f);
      isAttacking = false;
    }

    void OnTriggerStay(Collider other)
    {
      if(other.tag == "Weapon")
      {
        health.UpdateHealth(-1f);
      }
    }
}
