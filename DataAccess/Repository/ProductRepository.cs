using AutoMapper;
using BusinessModel.Entities;
using DataAccess.Dtos;
using DataAccess.Dtos.RequestModel;
using DataAccess.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

public class ProductRepository : IProductRepository
{
    private Ass3DbContext _context;
    private IMapper _mapper;

    public ProductRepository(Ass3DbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public List<ProductDto> GetProducts()
    {
        return _mapper.Map<List<ProductDto>>(_context.Products.Include(x => x.Category).ToList());
    }

    public ProductDto? GetProductById(int id)
    {
        return _mapper.Map<ProductDto>(_context.Products.Include(x => x.Category).FirstOrDefault(x => x.ProductId == id));
    }

    public void AddProduct(AddProductRequest request)
    {
        try
        {
            var product = _mapper.Map<Product>(request);
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateProduct(UpdateProductRequest request)
    {
        try
        {
            var product = _context.Products.FirstOrDefault(x => x.ProductId == request.ProductId);
            if (product == null)
                throw new Exception("Not found");
            product.ProductName = request.ProductName;
            product.CategoryId = request.CategoryId;
            product.UnitsInStock = request.UnitsInStock;
            product.Weight = request.Weight;
            product.UnitPrice = request.UnitPrice;
            _context.Products.Update(product);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteProduct(int id)
    {
        try
        {
            var product = _context.Products.FirstOrDefault(x => x.ProductId == id);
            if (product == null)
                throw new Exception("Not found");
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}