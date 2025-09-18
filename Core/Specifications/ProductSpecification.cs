using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecParams specParms) : base(x =>
        (string.IsNullOrEmpty(specParms.Search) || x.Name.ToLower().Contains(specParms.Search)) &&
        (specParms.Brands.Count == 0 || specParms.Brands.Contains(x.Brand)) &&
        (specParms.Types.Count == 0 || specParms.Types.Contains(x.Type))
    )
    {
        ApplyPaging(specParms.PageSize * (specParms.PageIndex - 1), specParms.PageSize);

        switch (specParms.Sort)
        {
            case "priceAsc":
                AddOrderBy(x => x.Price);
                break;
            case "priceDesc":
                AddOrderByDescending(x => x.Price);
                break;
            default:
                AddOrderBy(x => x.Name);
                break;
        }
    }

}
