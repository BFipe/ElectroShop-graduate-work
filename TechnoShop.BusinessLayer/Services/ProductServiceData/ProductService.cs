using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using System.Threading.Tasks;
using TechnoShop.BusinessLayer.Dtos.ProductDto;
using TechnoShop.BusinessLayer.Interfaces;
using TechnoShop.Data.Repositories.Interfaces;
using TechnoShop.Entities.ProductEntity;
using TechnoShop.Exceptions;
using TechnoShop.BusinessLayer.Dtos.ProductTypeDto;
using Microsoft.AspNetCore.Identity;
using TechnoShop.Entities.UserEntity;

namespace TechnoShop.BusinessLayer.Services.ProductServiceData
{
    public class ProductService : IProductService
    {
        private readonly UserManager<TechnoShopUser> _userManager;
        private readonly IProductRepository _productRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IProductTypeRepository productTypeRepository, IMapper mapper, UserManager<TechnoShopUser> userManager)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task AddNewProduct(ProductRequestDto requestProduct)
        {
            if (requestProduct.Count < 0) throw new IncorrectValueException<int>(requestProduct.Count);
            if (await _productRepository.IsExists(requestProduct.Name)) throw new ObjectExistsException(requestProduct.Name);
            if (await _productTypeRepository.IsExists(requestProduct.ProductTypeName) == false) throw new ObjectNotExistsException(requestProduct.ProductTypeName);
            var product = _mapper.Map<Product>(requestProduct);
            product.ProductId = Guid.NewGuid().ToString();
            product.ProductRate = 10;
            await _productRepository.Add(product);
            await _productRepository.Save();
        }

        public async Task AddNewType(ProductTypeRequestDto productTypeRequest)
        {
            if (String.IsNullOrWhiteSpace(productTypeRequest.TypeName)) throw new IncorrectValueException<string>();
            if (await _productTypeRepository.IsExists(productTypeRequest.TypeName)) throw new ObjectExistsException(productTypeRequest.TypeName);
            var productType = _mapper.Map<ProductType>(productTypeRequest);
            productType.ProductTypeId = Guid.NewGuid().ToString();
            await _productTypeRepository.Add(productType);
            await _productTypeRepository.Save();
        }

        public async Task<List<ProductResponceDto>> GetProducts()
        {
            List<ProductResponceDto> productResponceDtos = new();
            foreach (var product in _productRepository.GetAll())
            {
                productResponceDtos.Add(_mapper.Map<ProductResponceDto>(product));
            }
            return productResponceDtos;
        }

        public async Task<List<ProductTypeResponceDto>> GetProductTypes()
        {
            List<ProductTypeResponceDto> productTypeResponceDtos = new();
            foreach (var productType in _productTypeRepository.GetAll())
            {
                productTypeResponceDtos.Add(_mapper.Map<ProductTypeResponceDto>(productType));
            }
            return productTypeResponceDtos;
        }

        public async Task DeleteProduct(string productId)
        {
            await _productRepository.Delete(productId);
            await _productRepository.Save();
        }
    }
}
