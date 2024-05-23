using RollUpExercise.enums;

namespace RollUpExercise.models
{
    public class Gtin : Product
    {
        public Variant Variant { get; set; }
        public SizeEnum Size { get; set; }

        public Gtin(Variant variant, SizeEnum size, float? price)
        {
            Variant = variant;
            Size = size;
            Price = price;
        }
    }
}
