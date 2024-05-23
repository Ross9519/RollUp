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
               gtins.ForEach(g => g.Price = g.Price == -1 ? null : g.Price); 

            return results;
        }

        private static List<float> GetPriceExceptions<TKey,TElement>(IEnumerable<IGrouping<TKey,TElement>> groups)
            where TKey : Product
            where TElement : Product
            => groups.Select(group =>
                    {
                        group.Key.Price = group.Min(e => e.Price);
                        return group.Select(e => e.Price ?? 0).Where(p => p != group.Key.Price);
                    })
                    .SelectMany(priceList => priceList)
                    .ToList();
    }
}
