using UnityEngine;
using System.Collections;

public class VendingMachine : MonoBehaviour
{
    public int recoveryAmount = 10;

    // 한 번만 먹도록 방지
    private bool used = false;

    void Reset()
    {
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (used) return;
        if (!other.CompareTag("Player")) return;

        used = true;

        GameManager.Instance.RecoverMental(recoveryAmount);

        Animator playerAnim = other.GetComponent<Animator>();
        playerAnim.SetTrigger("EatCoffee");
    }
}
