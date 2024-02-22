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
            ((HeartContainer) A.HeartContainer().With(A.Heart().With(_target))).Replenish(0);

			Assert.That(_target.fillAmount, Is.EqualTo(0f));
		}

        [Test]
        public void _1_Sets_Image_With_0_Percent_Fill_To_25_Percent_Fill()
        {
            ((HeartContainer)A.HeartContainer().With(A.Heart().With(_target))).Replenish(1);

            Assert.That(_target.fillAmount, Is.EqualTo(0.25f));
        }

        [Test]
        public void _Empty_Hearts_Are_Replenished()
        {
            ((HeartContainer)A.HeartContainer().With(
                A.Heart().With(An.Image().WithFillAmount(1)), 
                A.Heart().With(_target))).Replenish(1);

            Assert.That(_target.fillAmount, Is.EqualTo(0.25f));
        }

        [Test]
        public void _Hearts_Are_Replenished_In_Order()
        {
            ((HeartContainer)A.HeartContainer().With(
                A.Heart(),
                A.Heart().With(_target))).Replenish(1);

            Assert.That(_target.fillAmount, Is.EqualTo(0f));
        }

        [Test]
        public void _Distributes_Heart_Pieces_Across_Multiple_Unfilled_Hearts()
        {
            ((HeartContainer)A.HeartContainer().With(
                A.Heart().With(An.Image().WithFillAmount(0.75f)),
                A.Heart().With(_target))).Replenish(2);

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
            ((HeartContainer)A.HeartContainer().With(A.Heart().With(_target))).Deplete(0);

            Assert.That(_target.fillAmount, Is.EqualTo(1f));
        }

        [Test]
        public void _1_Sets_Image_With_100_Percent_Fill_To_75_Percent_Fill()
        {
            ((HeartContainer)A.HeartContainer().With(A.Heart().With(_target))).Deplete(1);

            Assert.That(_target.fillAmount, Is.EqualTo(0.75f));
        }

        [Test]
        public void _2_Sets_Image_To_75_Percent_Fill_After_Distribution()
        {
            ((HeartContainer)A.HeartContainer().With(
                A.Heart().With(_target),
                A.Heart().With(An.Image().WithFillAmount(0.25f))
                )).Deplete(2);

            Assert.That(_target.fillAmount, Is.EqualTo(0.75f));
        }
    }
}