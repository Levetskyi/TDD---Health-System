using NUnit.Framework;
using UnityEngine;
using System;

public class PlayerTests : MonoBehaviour
{
    public class TheCurrentHealthProperty
    {
        [Test]
        public void Health_Defaults_To_0()
        {
            var player = new Player(0);

            Assert.That(player.CurrentHealth, Is.EqualTo(0));
        }

        [Test]
        public void Throws_Exception_When_Current_Health_Is_Less_Than_0()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Player(-1));
        }

        [Test]
        public void Throws_Exception_When_Current_Health_Is_Greater_Than_Maximum_Health()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Player(2, 1));
        }
    }

    public class TheHealMethod
    {
        [Test]
        public void _0_Does_Nothing()
        {
            var player = new Player(0);

            player.Heal(0);

            Assert.That(player.CurrentHealth, Is.EqualTo(0));
        }

        [Test]
        public void _1_Increments_Current_Health()
        {
            var player = new Player(0);

            player.Heal(1);

            Assert.That(player.CurrentHealth, Is.EqualTo(1));
        }

        [Test]
        public void OverHealing_Is_Ignored()
        {
            var player = new Player(0, 1);

            player.Heal(2);

            Assert.That(player.CurrentHealth, Is.EqualTo(1));
        }
    }

    public class TheDamageMethod
    {
        [Test]
        public void _0_Does_Nothing()
        {
            var player = new Player(1);

            player.TakeDamage(0);

            Assert.That(player.CurrentHealth, Is.EqualTo(1));
        }

        [Test]
        public void _1_Decrements_Current_Health()
        {
            var player = new Player(1);

            player.TakeDamage(1);

            Assert.That(player.CurrentHealth, Is.EqualTo(0));
        }

        [Test]
        public void OverDamaging_Is_Ignored()
        {
            var player = new Player(1);

            player.TakeDamage(2);

            Assert.That(player.CurrentHealth, Is.EqualTo(0));
        }
    }
}