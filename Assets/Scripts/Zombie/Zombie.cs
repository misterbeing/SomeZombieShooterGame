using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    public Transform playerTransform;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI helth;
    [SerializeField] private Canvas healthCanvas;
    public bool allowMovement;
    public float damageInflicts = 1f;
    public float experiencePoints = 10f;

    [SerializeField] private GameObject[] zombieMeshes;

    [SerializeField] private Animator animator;
    private float maximumHealth;

    private void OnEnable()
    {
        maximumHealth = maxHealth;
    }
    public void GotHit(bool val)
    {
        Debug.Log("Zombie got hit!");
        maxHealth -= 10f;
        healthBar.fillAmount = maxHealth / maximumHealth;
        helth.text = maxHealth.ToString();
        StartCoroutine(ShowHideCanvas());
        if (maxHealth <= 0)
        {
           StartCoroutine(Die());
        }
    }
    
    private IEnumerator Die()
    {
        Debug.Log("Zombie died!");
        GameManager.zombieSpawner.RemoveZombie(this);
        GameManager.onZombieExitingPlayersView?.Invoke(this);
        agent.enabled = false;
        this.GetComponent<Collider>().enabled = false;
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
        yield return null;
    }

    private IEnumerator ShowHideCanvas()
    {
        healthCanvas.GetComponent<CanvasGroup>().alpha = 1f;
        yield return new WaitForSeconds(2f);
        healthCanvas.GetComponent<CanvasGroup>().alpha = 0f;
        yield return null;
    }

    public void AttackPlayer(bool val)
    {
        allowMovement = !val;
        animator.SetBool("attack", val);
    }
}
