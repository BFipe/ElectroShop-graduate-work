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
using Microsoft.EntityFrameworkCore;

namespace TechnoShop.BusinessLayer.Services.ProductServiceData
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IProductTypeRepository productTypeRepository, IMapper mapper, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
            _userRepository = userRepository;
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

        public async Task<List<ProductResponceDto>> GetProducts(string userEmail, string productType, int page, int productsPerPage)
        {
            var productCount = GetProductCount(productType);

            if (page < 1) throw new IncorrectValueException<int>(page);
            if (productsPerPage < 1) throw new IncorrectValueException<int>(productsPerPage);
            if (productCount != 0 && productCount + productsPerPage <= page * productsPerPage)
            {
                throw new OutOfRangeException(page);
            }
            
            var user = await _userRepository.FindUserByEmail(userEmail);
            var productResponceDtos = _productRepository
                .GetAll()
                .Where(q => productType == null || q.ProductTypeName == productType).ToList()
                .Skip(--page * productsPerPage).Take(productsPerPage)
                .Select(q => _mapper.Map<ProductResponceDto>(q))
                .ToList();
            productResponceDtos.ForEach(q =>
            {
                if (user != null && user.Products.Any(j => j.ProductId == q.ProductId)) q.IsOpenForCart = false;
            });
            return productResponceDtos;
        }

        public async Task<List<ProductTypeResponceDto>> GetProductTypes()
        { 
            return _mapper.Map<List<ProductTypeResponceDto>>(_productTypeRepository.GetAll().ToList());
        }

        public async Task DeleteProduct(string productId)
        {
            await _productRepository.Delete(productId);
            await _productRepository.Save();
        }

        public int GetProductCount(string productType)
        {
            return _productRepository.GetAll().Where(q => productType == null || q.ProductTypeName == productType).Count();
        }
    }
}
