using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHearts : MonoBehaviour
{
    [SerializeField]
    [Range(0, 6)]
    private int health;
    [SerializeField]
    [Range(0,6)]
    private int maxHealth;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    /// <summary>
    /// /////////////////////////////////////////////tozi skript da se oprawi, dali e dobre samo update da sedi tuk ?
    /// </summary>
    void Start()
    {
        //health = maxHealth;
        //for (int i = 0; i < hearts.Length; i++)
        //{
        //    if (i < maxHealth)
        //        hearts[i].enabled = true;
        //    else
        //        hearts[i].enabled = false;
        //}
        //UpdateHearts();
    }

    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < maxHealth)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }
    //public void TakeDamage(int damage)
    //{
    //    health -= damage;
    //    if (health < 0) health = 0;
    //    UpdateHearts();
    //}

    //public void Heal(int amount)
    //{
    //    health += amount;
    //    if (health > maxHealth) health = maxHealth;
    //    UpdateHearts();
    //}

    //void UpdateHearts()
    //{
    //    for (int i = 0; i < hearts.Length; i++)
    //    {
    //        if (i < health)
    //            hearts[i].sprite = fullHeart;
    //        else
    //            hearts[i].sprite = emptyHeart;
    //    }
    //}
}

