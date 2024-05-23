using RollUpExercise.enums;

namespace RollUpExercise.models
{
    public class Variant : Product
    {
        public Product Product { get; set; }
        public ColorEnum Color { get; set; }

        public Variant(Product product, ColorEnum color)
        {
            Product = product;
            Color = color;
        }
    }
}
