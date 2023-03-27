using NUnit.Framework;
using UnityEngine;

public class CharacterTests
{

    [Test]
    public void TakeDamage_ReduceHealthPointsByGivenAmount()
    {
        // ARRANGE
        GameObject gameObject = new GameObject("Character", typeof(Character));
        Character character = gameObject.GetComponent<Character>();
        character.healthPoints = 50f;

        // ACT
        character.TakeDamage(20f);

        // ASSERT
        Assert.AreEqual(30f, character.healthPoints);
    }

    [Test]
    public void TakeDamage_WontReduceHealthPointsBelowZero()
    {
        // ARRANGE
        GameObject gameObject = new GameObject("Character", typeof(Character));
        Character character = gameObject.GetComponent<Character>();
        character.healthPoints = 50f;

        // ACT
        character.TakeDamage(100f);

        // ASSERT
        Assert.AreEqual(0f, character.healthPoints);
    }

    [Test]
    public void Attack_DealsAttackPointsAmountOfDamageToTarget()
    {
        // ARRANGE
        GameObject gameObject = new GameObject("Character", typeof(Character));
        GameObject gameObject2 = new GameObject("Character", typeof(Character));
        Character character = gameObject.GetComponent<Character>();
        Character targetCharacter = gameObject2.GetComponent<Character>();
        targetCharacter.healthPoints = 50f;
        character.attackPoints = 10f;

        // ACT
        character.Attack(targetCharacter);

        // ASSERT
        Assert.AreEqual(40f, targetCharacter.healthPoints);
    }

}
