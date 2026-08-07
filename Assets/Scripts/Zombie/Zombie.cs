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

    [SerializeField] private Animator animator;
    public void GotHit()
    {
        Debug.Log("Zombie got hit!");
        maxHealth -= 10f;
        healthBar.fillAmount = maxHealth / 100f;
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
}
