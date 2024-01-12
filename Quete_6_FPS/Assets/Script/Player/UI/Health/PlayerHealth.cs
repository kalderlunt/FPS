using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float _health;
    private float _lerpTimer;
    
    [Header("Health Bar")]
    public float maxhealth = 100.0f;
    public float chipSpeed = 2.0f;
    public Image backHealthBar;
    public Image frontHealthBar;

    [Header("Damage Overlay")]
    public Image overlay;   // our Damageoverlay Gameobject
    public float duration = 2.0f;  // how long the image stays fully opaque
    public float fadeSpeed = 1.5f; // how quickly the image will fade

    private float _durationTimer; // timer to check against the duration 


    // Start is called before the first frame update 

    void Start()
    {
        _health = maxhealth;
        frontHealthBar.fillAmount = _health / maxhealth;
        backHealthBar.fillAmount = _health / maxhealth;

        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        _health = Mathf.Clamp(_health, 0, maxhealth);
        UpdateHealthUI();
        if (overlay.color.a > 0)
        {
            if (_health < 30)
                return; //to keep overlay 

            _durationTimer += Time.deltaTime;
            if (_durationTimer > duration)
            {
                // fade the image
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }

    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = _health / maxhealth;  // decimal 0 to 1
        
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            _lerpTimer += Time.deltaTime;
            
            float percentComplete = _lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;

            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            _lerpTimer += Time.deltaTime;

            float percentComplete = _lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;

            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _lerpTimer = 0.0f;
        _durationTimer = 0.0f;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
    }

    public void RestoreHealth(float healAmount)
    {
        _health += healAmount;
        _lerpTimer = 0.0f;
    }
}