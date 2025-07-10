using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public bool isMoveFollow;
    [SerializeField] private GameObject parent;
    private HookController hookController;
    [SerializeField] private float pullSpeed;
    [SerializeField] private int score, explosive;
    [SerializeField] private AudioClip clip;

    [SerializeField] private bool isBag;
    public GameObject explosionEffect;
    void Start()
    {
        isMoveFollow = false;
        hookController = FindObjectOfType<HookController>();
        if (isBag)
        {
            score = Random.Range(200, 500);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == parent)
        {
            if(clip != null)
            {
                AudioManager.instance.PlayItemTouch(clip);
            }
            isMoveFollow = true;
            hookController.isDropping = false;
            hookController.isPulling = true;
            hookController.pullSpeed = pullSpeed;
            AudioManager.instance.PlayPullSound();
        }
    }
    private void FixedUpdate()
    {
        MoveFollow(parent);
    }
    private void MoveFollow(GameObject target)
    {
        if (isMoveFollow)
        {
            
            transform.position = new Vector3(target.transform.position.x,
                target.transform.position.y - gameObject.GetComponent<Collider2D>().bounds.extents.y,
                target.transform.position.z);

            if (hookController.isThrowExplosive)
            {
                AudioManager.instance.PlayExplosiveSound();
                GameObject boom = Instantiate(explosionEffect,transform.position, Quaternion.identity );
                Destroy(boom, 0.3f);
                Destroy(gameObject);
                GameManager.instance.explosive -= 1;
                hookController.pullSpeed = hookController.initPullSpeed;
            }

            if(hookController.isSwaying)
            {
                AudioManager.instance.PlayMoneySound();
                Destroy(gameObject);
                GameManager.instance.score += score;
                GameManager.instance.explosive += explosive;
            }
        }
    }
}
