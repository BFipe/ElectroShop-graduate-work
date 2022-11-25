using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using System.Threading.Tasks;
using TechnoShop.BusinessLayer.Dtos.ProductDto;
using TechnoShop.BusinessLayer.Interfaces;
using TechnoShop.Data.Repositories.Interfaces;
using TechnoShop.Entities.Enums;
using TechnoShop.Entities.ProductEntity;
using TechnoShop.Exceptions;

namespace TechnoShop.BusinessLayer.Services.ProductServiceData
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task AddNewProduct(ProductRequestDto requestProduct)
        {
            if (_productRepository.IsAlreadyExists(requestProduct.Name)) throw new ObjectExistsException(requestProduct.Name);
            var product = _mapper.Map<Product>(requestProduct);
            product.ProductId = Guid.NewGuid();
            await _productRepository.Add(product);
            await _productRepository.Save();
        }

        public async Task<List<ProductRequestDto>> GetProducts()
        {
            List<ProductRequestDto> productRequestDtos = new();
            foreach (var product in _productRepository.GetAll())
            {
                productRequestDtos.Add(_mapper.Map<ProductRequestDto>(product));
            }
            return productRequestDtos;
        }
    }
}
