using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.UI;
using UnityEngine;

public class HeartContainerTests : MonoBehaviour
{
	public class TheReplenishMethod
	{
		protected Image _target;

		[SetUp]
		public void BeforeTests()
		{
			_target = An.Image(); 
		}

		[Test]
		public void _0_Sets_Image_With_0_Fill_To_0_Fill()
		{
            var heartContainer = new HeartContainer(new List<Heart> { new(_target) });
            heartContainer.Replenish(new HealEvent { HealAmount = 0 });

            Assert.That(_target.fillAmount, Is.EqualTo(0f));
		}

        [Test]
        public void _1_Sets_Image_With_0_Percent_Fill_To_25_Percent_Fill()
        {
            var heartContainer = new HeartContainer(new List<Heart> { new(_target) });
            heartContainer.Replenish(new HealEvent { HealAmount = 1 });

            Assert.That(_target.fillAmount, Is.EqualTo(0.25f));
        }

        [Test]
        public void Empty_Hearts_Are_Replenished()
        {
            var image = An.Image().WithFillAmount(1f);

            var heartContainer = new HeartContainer(new List<Heart> {
                new(image),
                new(_target) });
            heartContainer.Replenish(new HealEvent { HealAmount = 1 });

            Assert.That(_target.fillAmount, Is.EqualTo(0.25f));
        }

        [Test]
        public void Hearts_Are_Replenished_In_Order()
        {
            var image = An.Image();

            var heartContainer = new HeartContainer(new List<Heart> {
                new(image),
                new(_target) });
            heartContainer.Replenish(new HealEvent { HealAmount = 1 });

            Assert.That(_target.fillAmount, Is.EqualTo(0f));
        }

        [Test]
        public void Distributes_Heart_Pieces_Across_Multiple_Unfilled_Hearts()
        {
            var image = An.Image().WithFillAmount(0.75f);

            var heartContainer = new HeartContainer(new List<Heart> {
                new(image),
                new(_target) });
            heartContainer.Replenish(new HealEvent { HealAmount = 2 });

            Assert.That(_target.fillAmount, Is.EqualTo(0.25f));
        }
    }

    public class TheDepleteMethod
    {
        protected Image _target;

        [SetUp]
        public void BeforeTests()
        {
            _target = An.Image().WithFillAmount(1f);
        }

        [Test]
        public void _0_Sets_Image_With_100_Percent_Fill_To_100_Percent_Fill()
        {
            var heartContainer = new HeartContainer(new List<Heart> {
                new(_target) });
            heartContainer.Deplete(new DamageEvent { DamageAmount = 0 });

            Assert.That(_target.fillAmount, Is.EqualTo(1f));
        }

        [Test]
        public void _1_Sets_Image_With_100_Percent_Fill_To_75_Percent_Fill()
        {
            var heartContainer = new HeartContainer(new List<Heart> {
                new(_target) });
            heartContainer.Deplete(new DamageEvent { DamageAmount = 1 });

            Assert.That(_target.fillAmount, Is.EqualTo(0.75f));
        }

        [Test]
        public void _2_Sets_Image_To_75_Percent_Fill_After_Distribution()
        {
            var image = An.Image().WithFillAmount(0.25f);

            var heartContainer = new HeartContainer(new List<Heart> {
                new(_target),
                new(image) });
            heartContainer.Deplete(new DamageEvent { DamageAmount = 2 });

            Assert.That(_target.fillAmount, Is.EqualTo(0.75f));
        }
    }
}