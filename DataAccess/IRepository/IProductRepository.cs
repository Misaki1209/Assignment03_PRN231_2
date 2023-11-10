using BusinessModel.Entities;
using DataAccess.Dtos;
using DataAccess.Dtos.RequestModel;

namespace DataAccess.IRepository;

public interface IProductRepository
{
    public List<ProductDto> GetProducts();
    public ProductDto? GetProductById(int id);
    public void AddProduct(AddProductRequest request);
    public void UpdateProduct(UpdateProductRequest request);
    public void DeleteProduct(int id);
}