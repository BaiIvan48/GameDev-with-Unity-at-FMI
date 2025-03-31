using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTests
{
    private GameObject player;
    private GameObject enemy;
    private GameObject trampoline;

    [SetUp]
    public void Setup()
    {
        player = new GameObject("Player");
        player.tag = "Player";
        player.AddComponent<Rigidbody2D>();
        player.AddComponent<BoxCollider2D>();
        player.AddComponent<Health>();

        enemy = new GameObject("Enemy");
        enemy.tag = "Enemy";
        enemy.AddComponent<BoxCollider2D>();
        enemy.AddComponent<Rigidbody2D>();

        trampoline = new GameObject("Trampoline");
        trampoline.AddComponent<BoxCollider2D>().isTrigger = true;
        trampoline.AddComponent<Trampoline>();
    }

    [UnityTest]
    public IEnumerator PlayerLosesLifeWhenHitByEnemy()
    {
        var health = player.GetComponent<Health>();
        int initialHealth = health.getValue();

        player.transform.position = Vector3.zero;
        enemy.transform.position = Vector3.zero;
        yield return new WaitForFixedUpdate();

        Assert.Less(health.getValue(), initialHealth, "Player loses heath after enemy hit.");
    }

    [UnityTest]
    public IEnumerator PlayerJumpsWhenLandingOnTrampoline()
    {
        var rb = player.GetComponent<Rigidbody2D>();
        float initialY = player.transform.position.y;

        player.transform.position = new Vector3(0, 2, 0);
        trampoline.transform.position = Vector3.zero;

        yield return new WaitForSeconds(0.5f);

        Assert.Greater(player.transform.position.y, initialY, "Player on trampoline.");
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(player);
        Object.Destroy(enemy);
        Object.Destroy(trampoline);
    }
}
