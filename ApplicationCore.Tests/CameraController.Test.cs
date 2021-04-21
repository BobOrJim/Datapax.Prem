using System;
using System.IO;
using System.Threading;
using Xunit;
using ApplicationCore.Cam;
using GlobalStringsReadOnly;
using Infrastructure;
//https://xunit.net/docs/comparisons



namespace Presentation.Tests
{
    public class CameraControllerTest
    {
        private readonly DataAccess dataAccess;


        [Fact]
        public void TrueIsTrue()
        {
            Assert.True(true);
        }

        //MethodName_StateOrActionInTest_ExpectedBehavior
        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void RunCamera_StartingCamera_CammeraIsRunning(Boolean startCamera, bool expected)
        {
            //Arrange
            CameraController camera1 = new CameraController(dataAccess, 0, "Camera1"); //Note, dataacces is not used here, i.e its ok that it is null.

            // Act
            camera1.RunCamera(startCamera);

            // Assert
            bool actual = camera1.videoSource.IsRunning;
            Assert.Equal(expected, actual);
        }
    }
}









//using NUnit.Framework;
//using Calculator;

//namespace CalculatorTest
//{
//    public class TermParserTests
//    {
//        private TermParser _parser;

//        [SetUp]
//        public void Setup()
//        {
//            _parser = new TermParser();
//            _parser.RegisterSymbol('+', _parser.MakeAdditionTerm);
//            _parser.RegisterSymbol('*', _parser.MakeMultiplicationTerm);
//            _parser.RegisterSymbol(TermParser.SUBTRACTIONSUBSTITUTE, _parser.MakeSubtractionTerm);
//            _parser.RegisterSymbol('/', _parser.MakeDivisionTerm);
//        }

//        [Test]
//        public void One_Plus_Two_Should_Evaluate_To_Three()
//        {
//            var input = "1 + 2";
//            var result = _parser.Parse(input).Calculate();
//            Assert.AreEqual(3.0m, result);
//        }

//        [Test]
//        public void Empty_String_Should_Evaluate_To_Zero()
//        {
//            var input = "";
//            var result = _parser.Parse(input).Calculate();
//            Assert.AreEqual(0.0m, result);
//        }

//        [Test]
//        public void One_Star_One_Should_Evaluate_To_One()
//        {
//            var input = "1 * 1";
//            var result = _parser.Parse(input).Calculate();
//            Assert.AreEqual(1.0m, result);
//        }

//        [Test]
//        public void Three_Star_MinusEleven_Should_Evaluate_To_MinusThirtyThree()
//        {
//            var input = "3 * -11";
//            var result = _parser.Parse(input).Calculate();
//            Assert.AreEqual(-33.0m, result);
//        }

//        [Test]
//        public void Three_Times_Three_Plus_Eight_Should_Evaluate_To_Seventeen()
//        {
//            var input = "3 * 3 + 8";
//            var result = _parser.Parse(input).Calculate();
//            Assert.AreEqual(17.0m, result);
//        }

//        [Test]
//        public void One_Minus_Eight_Should_Evaluate_To_Minus_Seven()
//        {
//            var input = "1 - 8";
//            var result = _parser.Parse(input).Calculate();
//            Assert.AreEqual(-7.0m, result);
//        }

//        [Test]
//        public void Ten_Divided_By_Two_Should_Evaluate_To_Five()
//        {
//            var input = "10 / 2";
//            var result = _parser.Parse(input).Calculate();
//            Assert.AreEqual(5.0m, result);
//        }

//        [Test]
//        public void Characters_Mixed_Into_An_Expression_Should_Be_Ignored()
//        {
//            var input = "asd10asd+dsagfg2sdffgf";
//            var result = _parser.Parse(input).Calculate();
//            Assert.AreEqual(12.0m, result);
//        }

//        [Test]
//        public void Parantheshis_Should_Be_Ignored()
//        {
//            var input = "4 * (3 - 3)";
//            var result = _parser.Parse(input).Calculate();
//            Assert.AreEqual(4 * 3 - 3, result);
//        }

//        [Test]
//        public void Five_Plus_Six_Times_Two_Minus_Three_Divided_By_Two_Should_Evaluate_To_Fifteen_Point_Five()
//        {
//            var input = "5 + 6 * 2 - 3 / 2";
//            var result = _parser.Parse(input).Calculate();
//            Assert.AreEqual(15.5m, result);
//        }

//        [Test]
//        public void Attempt_To_Divide_By_Zero_Should_Result_In_An_Exception()
//        {
//            var input = "1 / 0";
//            try
//            {
//                var result = _parser.Parse(input).Calculate();
//            }
//            catch (DivideByZeroException e)
//            {
//                Assert.True(true);
//                return;
//            }
//            Assert.Fail();
//        }
//        [Test]
//        public void Zero_Point_Five_Times_Two_Should_Evaluate_To_One()
//        {
//            var input = "0,5 * 2";
//            var result = _parser.Parse(input).Calculate();
//            Assert.AreEqual(1.0m, result);
//        }

//        [Test]
//        public void Subtraction_Should_Only_Apply_To_The_Immediate_Right_Term()
//        {
//            var input = "10 - 5 + 5";
//            var result = _parser.Parse(input).Calculate();
//            Assert.AreEqual(10, result);
//        }
//    }
//}