using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.DTOs;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IProductImagesService
    {
        int CreateNewImage(List<IFormFile> files, Product product);
        List<ProductImage> GetImagesForProduct(string productId);
    }
    
    public class ProductImagesService : IProductImagesService
    {
        private readonly SadboisContext _context;
        private readonly IMapper _mapper;

        public ProductImagesService(SadboisContext context)
        {
            _context = context;
        }
        
        public int CreateNewImage(List<IFormFile> files, Product product)
        {
            foreach (var file in files)
            {
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                ProductImage img = new ProductImage
                {
                    ImageData = ms.ToArray(),
                    Product = product
                };
                ms.Close();
                ms.Dispose();

                _context.ProductImages.Add(img);
            }
            return _context.SaveChanges();
        }

        public List<ProductImage> GetImagesForProduct(string productId)
        {
            return _context.ProductImages
                .Where(x => x.ProductId.ToString().Equals(productId))
                .ToList();
        }
    }
}