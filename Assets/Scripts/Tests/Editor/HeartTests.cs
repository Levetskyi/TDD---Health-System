using NUnit.Framework;
using UnityEngine.UI;
using UnityEngine;
using System;

public class HeartTests
{
    private Image _image;
    private Heart _heart;

    [SetUp]
    public void BeforeEveryTest()
    {
        _image = new GameObject().AddComponent<Image>();
        _heart = new Heart(_image);
    }

    public class TheEmptyHeartPiecesProperty : HeartTests
    {
        [Test]
        public void _100_Percent_Fill_Is_0_Empty_Heart_Pieces()
        {
            _image.fillAmount = 1f;

            Assert.That(_heart.EmptyHeartPieces, Is.EqualTo(0));
        }

        [Test]
        public void _75_Percent_Fill_Is_1_Empty_Heart_Pieces()
        {
            _image.fillAmount = 0.75f;

            Assert.That(_heart.EmptyHeartPieces, Is.EqualTo(1));
        }
    }

    public class TheFilledHeartPiecesProperty : HeartTests
    {
        [Test]
        public void _0_Image_Fill_Is_0_Heart_Pieces()
        {
            _image.fillAmount = 0f;

            Assert.That(_heart.FilledHeartPieces, Is.EqualTo(0));
        }

        [Test]
        public void _25_Percent_Image_Fill_Is_1_Heart_Piece()
        {
            _image.fillAmount = 0.25f;

            Assert.That(_heart.FilledHeartPieces, Is.EqualTo(1));
        }

        [Test]
        public void _75_Percent_Image_Fill_Is_3_Heart_Piece()
        {
            _image.fillAmount = 0.75f;

            Assert.That(_heart.FilledHeartPieces, Is.EqualTo(3));
        }
    }

    public class ReplenishMethod : HeartTests
    {
        [Test]   
        public void _0_Sets_Image_With_0_Fill_To_0_Fill() 
        {
            _image.fillAmount = 0f;

            _heart.Replenish(0);

            Assert.That(_image.fillAmount, Is.EqualTo(0));
        }

        [Test]
        public void _1_Sets_Image_With_0_Fill_To_25_Percent_Fill()
        {
            _image.fillAmount = 0f;

            _heart.Replenish(1);

            Assert.That(_image.fillAmount, Is.EqualTo(0.25f));
        }

        [Test]
        public void _1_Sets_Image_With_25_Percent_Fill_To_50_Percent_Fill()
        {
            _image.fillAmount = 0.25f;

            _heart.Replenish(1);

            Assert.That(_image.fillAmount, Is.EqualTo(0.5f));
        }

        [Test]
        public void Throws_Exception_For_Negative_Numbers_Of_Heart_Pieces()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _heart.Replenish(-1));
        }
    }

    public class DepleteMethod : HeartTests
    {
        [Test]
        public void _0_Sets_Image_With_100_Percent_Fill_To_100_Percent_Fill()
        {
            _image.fillAmount = 1f;

            _heart.Deplete(0);

            Assert.That(_image.fillAmount, Is.EqualTo(1f));
        }

        [Test]
        public void _1_Sets_Image_With_100_Percent_Fill_To_75_Percent_Fill()
        {
            _image.fillAmount = 1f;

            _heart.Deplete(1);

            Assert.That(_image.fillAmount, Is.EqualTo(0.75f));
        }

        [Test]
        public void _2_Sets_Image_With_75_Percent_Fill_To_25_Percent_Fill()
        {
            _image.fillAmount = 0.75f;

            _heart.Deplete(2);

            Assert.That(_image.fillAmount, Is.EqualTo(0.25f));
        }

        [Test]
        public void Throws_Exception_For_Negative_Numbers_Of_Heart_Pieces()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _heart.Deplete(-1));
        }
    }
}