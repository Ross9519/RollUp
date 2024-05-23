using RollUpExercise.enums;
using RollUpExercise.helpers;
using RollUpExercise.models;

namespace RollUpTest
{
    public class UnitTest1
    {
        [Fact]
        public void Should_Return_RollUp_Price_When_Prices_Are_Equals()
        {
            Product product = new();

            List<Variant> variants =
            [
                new(product, ColorEnum.Orange),
                new(product, ColorEnum.Green)
            ];

            List<Gtin> gtins =
            [
                new(variants[0], SizeEnum.L, 100),
                new(variants[0], SizeEnum.S, 100),
                new(variants[1], SizeEnum.XL, 100),
                new(variants[1], SizeEnum.M, 100),
            ];

            var prices = ProductHelper.RollUp(gtins);
            Assert.Equal([100], prices);
        }

        [Fact]
        public void Should_Return_RollUp_Price_And_Maxs_When_Prices_Are_Not_Equals()
        {
            Product product = new();

            List<Variant> variants =
            [
                new(product, ColorEnum.Orange),
                new(product, ColorEnum.Green)
            ];

            List<Gtin> gtins =
            [
                new(variants[0], SizeEnum.L, 50),
                new(variants[0], SizeEnum.S, 100),
                new(variants[1], SizeEnum.XL, 100),
                new(variants[1], SizeEnum.M, 100),
            ];

            var prices = ProductHelper.RollUp(gtins);
            Assert.Equal([100, 100, 50], prices);
        }

        [Fact]
        public void Should_Return_RollUp_Price_And_Maxs_When_All_Prices_Are_Not_Equals()
        {
            Product product = new();

            List<Variant> variants =
            [
                new(product, ColorEnum.Orange),
                new(product, ColorEnum.Green)
            ];

            List<Gtin> gtins =
            [
                new(variants[0], SizeEnum.L, 50),
                new(variants[0], SizeEnum.S, 70),
                new(variants[1], SizeEnum.XL, 100),
                new(variants[1], SizeEnum.M, 90)
            ];

            var prices = ProductHelper.RollUp(gtins);
            Assert.Equal([70, 100, 90, 50], prices);
        }

        [Fact]
        public void Should_Return_RollUp_PriceAnd_Maxs_When_Prices_Are_Equals_With_Five_Gtins()
        {
            Product product = new();

            List<Variant> variants =
            [
                new(product, ColorEnum.Orange),
                new(product, ColorEnum.Green)
            ];

            List<Gtin> gtins =
            [
                new(variants[0], SizeEnum.L, 100),
                new(variants[0], SizeEnum.S, 100),
                new(variants[1], SizeEnum.XL, 100),
                new(variants[1], SizeEnum.M, 100),
                new(variants[1], SizeEnum.XXL, 100)
            ];

            var prices = ProductHelper.RollUp(gtins);
            Assert.Equal([100], prices);
        }

        [Fact]
        public void Should_Return_RollUp_PriceAnd_Maxs_When_Prices_Are_Equals_With_Five_Gtins_And_Three_Variants()
        {
            Product product = new();

            List<Variant> variants =
            [
                new(product, ColorEnum.Orange),
                new(product, ColorEnum.Green),
                new(product, ColorEnum.Purple)
            ];

            List<Gtin> gtins =
            [
                new(variants[0], SizeEnum.L, 100),
                new(variants[0], SizeEnum.S, 100),
                new(variants[1], SizeEnum.XL, 100),
                new(variants[1], SizeEnum.M, 100),
                new(variants[2], SizeEnum.XXL, 100)
            ];

            var prices = ProductHelper.RollUp(gtins);
            Assert.Equal([100], prices);
        }

        [Fact]
        public void Should_Return_RollUp_PriceAnd_Maxs_When_Prices_Are_Equals_With_Five_Gtins_And_Three_Variants_And_Two_Products()
        {
            Product product1 = new();
            Product product2 = new();

            List<Variant> variants =
            [
                new(product1, ColorEnum.Orange),
                new(product1, ColorEnum.Green),
                new(product2, ColorEnum.Purple)
            ];

            List<Gtin> gtins =
            [
                new(variants[0], SizeEnum.L, 100),
                new(variants[0], SizeEnum.S, 100),
                new(variants[1], SizeEnum.XL, 100),
                new(variants[1], SizeEnum.M, 100),
                new(variants[2], SizeEnum.XXL, 100)
            ];

            var prices = ProductHelper.RollUp(gtins);
            Assert.Equal([100, 100], prices);
        }

        [Fact]
        public void Should_Manage_Null_Prices()
        {
            Product product = new();

            List<Variant> variants =
            [
                new(product, ColorEnum.Orange),
                new(product, ColorEnum.Green)
            ];

            List<Gtin> gtins =
            [
                new(variants[0], SizeEnum.L, null),
                new(variants[0], SizeEnum.S, 100),
                new(variants[1], SizeEnum.XL, 100),
                new(variants[1], SizeEnum.M, 100),
            ];

            var prices = ProductHelper.RollUp(gtins);
            Assert.Equal([100, 100], prices);
        }
    }
}