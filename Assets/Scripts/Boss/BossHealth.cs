using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private BossController bossController;
    public GameObject boss;
    public SpriteRenderer sprite;
    public Slider healthSlider; // Reference to the Slider component for health
    private int currentHealth;
    private Flash flash;
    public bool isAlive = true;
    private GameObject shadow;

    public Vector2 direction;

    public int DEF;
    const string HEALTH_SLIDER_TEXT = "Boss Health Slider";

    private void Awake()
    {
        flash = GetComponent<Flash>();
        bossController = GetComponent<BossController>();
        shadow = transform.Find("Shadow")?.gameObject;
    }

    private void Start()
    {
        SetPhysicsProcess(false);
        currentHealth = maxHealth;
        UpdateHealthSlider();
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
        }

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    private void Update()
    {
        direction = boss.transform.position - transform.position;
        sprite.flipX = direction.x < 0;
    }

    private void FixedUpdate()
    {
        Vector2 velocity = direction.normalized * 40f * Time.fixedDeltaTime;
        MoveAndCollide(velocity);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage - DEF;
        healthSlider.value = currentHealth; // Update the slider value

        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDeathRoutine());
    }

    public IEnumerator CheckDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());

        if (currentHealth <= 0)
        {

            healthSlider.gameObject.SetActive(false); // Hide the slider when health is zero
            if (shadow != null)
            {
                shadow.SetActive(false); // Disable the Shadow GameObject
            }
            isAlive = false;
            bossController.ChangeState(bossController.deathState);
        }
        else if (currentHealth <= healthSlider.maxValue / 2 && DEF == 0)
        {
            DEF = 5;
            bossController.ChangeState(bossController.armorBuffState);
        }
    }

    private void SetPhysicsProcess(bool value)
    {
        enabled = value;
    }

    private void MoveAndCollide(Vector2 velocity)
    {
        transform.Translate(velocity);
    }
}
