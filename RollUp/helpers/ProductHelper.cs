using RollUpExercise.models;

namespace RollUpExercise.helpers
{
    public static class ProductHelper
    {
        public static List<float> RollUp(List<Gtin> gtins)
        {
            List<float> results = [];

            gtins.ForEach(g => g.Price = g.Price is null ? -1 : g.Price);

            var groupedVariants = gtins.GroupBy(g => g.Variant);
            results.AddRange(GetPriceExceptions(groupedVariants));

            var groupedProducts = groupedVariants.Select(gr => gr.Key).GroupBy(v => v.Product);
            results.AddRange(GetPriceExceptions(groupedProducts));

            var productPrices = groupedProducts.Select(gr => gr.Key.Price ?? 0);
            
            if (!productPrices.Contains(-1))
                results.AddRange(productPrices);
            else
               gtins.ForEach(g => g.Price = g.Price is null ? -1 : g.Price); 

            return results;
        }

        private static List<float> GetPriceExceptions<TKey,TElement>(IEnumerable<IGrouping<TKey,TElement>> group)
            where TKey : Product
            where TElement : Product
            => group.Select(pair =>
                    {
                        pair.Key.Price = pair.Min(g => g.Price);
                        return pair.Select(g => g.Price ?? 0).Where(p => p != pair.Key.Price);
                    })
                    .SelectMany(priceList => priceList)
                    .ToList();
    }
}
