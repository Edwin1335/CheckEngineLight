using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [Header("Health")]
    public float _health;
    [Header("KnockBack")]
    [SerializeField] private float vertKnockback = 100;
    [SerializeField] private float horKnockback = 100;
    [Header("Attacked")]
    [SerializeField] private GameObject deathParticlePrefab;
    [SerializeField] private SpriteRenderer[] bodyParts;
    [SerializeField] private Color hurtColor;
    [SerializeField] private Color originalColor;

    [HideInInspector]
    public string enemyName;

    // Start is called before the first frame update
    void Start()
    {
        if (bodyParts.Length > 0)
        {
            originalColor = bodyParts[0].GetComponent<SpriteRenderer>().color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_health <= 0)
        {
            Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
            CameraShake.Instance.ShakeCamera(8f, .2f);

            // Play sound based on enemy.
            if (enemyName == "Rocky")
            {
                FindObjectOfType<AudioManager>().Play("RockyDead");
            }
            else if (enemyName == "Pikeem")
            {
                FindObjectOfType<AudioManager>().Play("PikeemDead");
            }
            else if (enemyName == "Tabee")
            {
                int choose = Random.Range(0, 2);
                if (choose == 0)
                {
                    FindObjectOfType<AudioManager>().Play("TabeDead1");
                }
                else
                {
                    FindObjectOfType<AudioManager>().Play("TabeeDead2");

                }
            }
            else if(enemyName == "Scorpion")
            {
                FindObjectOfType<AudioManager>().Play("ScorpionDies");

            }

            Destroy(gameObject, 0.0f);
        }
    }

    public void EnemyDamage(float _damage)
    {
        // Lower enemies health
        _health -= _damage;
        // Cheange enemie's clor when hit.
        if (bodyParts.Length > 0)
        {
            StartCoroutine(flash());
        }
    }

    public void knockBack(Transform tran)
    {
        if (tran.position.x < this.transform.position.x)
        {
            this.GetComponent<Rigidbody2D>().AddForce(transform.up * vertKnockback + transform.right * horKnockback);

        }
        else
        {
            this.GetComponent<Rigidbody2D>().AddForce(transform.up * vertKnockback + (transform.right * horKnockback) * -1);
        }
    }

    // public void setName(string name)
    // {
    //     this.enemyName = name;
    // }

    IEnumerator flash()
    {

        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].color = hurtColor;
        }
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].color = originalColor;
        }
    }
}
