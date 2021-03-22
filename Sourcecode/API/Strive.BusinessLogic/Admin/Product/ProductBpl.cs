﻿using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Product;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel.Product;
using Strive.BusinessLogic.Document;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Strive.BusinessLogic
{
    public class ProductBpl : Strivebase, IProductBpl
    {
        public ProductBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }

        public Result AddProduct(ProductAddDto product)
        {
            string error = string.Empty;
            var prodRal = new ProductRal(_tenant);
            foreach (var prod in product.Product)
            {
                if (!string.IsNullOrEmpty(prod.Product.Base64))
                    (error, prod.Product.FileName, prod.Product.ThumbFileName) = UploadImage(prod.Product.Base64, prod.Product.FileName);
                if (prodRal.AddProduct(prod) > 0)
                    continue;
                else
                {
                    error = "Error inserting product.!";
                    break;
                }
            }

            if (error == string.Empty)
            {
                return ResultWrap(true, "Status", "Success");
            }
            else
            {
                return Helper.ErrorMessageResult(error);
            }
        }

        public Result GetProductSearch(ProductSearchDto search)
        {
            return ResultWrap(new ProductRal(_tenant).GetProductSearch, search, "ProductSearch");
        }

        public Result UpdateProduct(Product product)
        {
            string error = string.Empty;
            (error, product.FileName, product.ThumbFileName) = UploadImage(product.Base64, product.FileName);

            return ResultWrap(new ProductRal(_tenant).UpdateProduct, product, "Status");
        }

        public Result GetAllProduct()
        {
            var products = new ProductRal(_tenant).GetAllProduct();

            foreach (var prod in products)
            {
                string fileName = string.Empty;

                fileName = prod.ThumbFileName;

                if (string.IsNullOrEmpty(fileName))
                    fileName = prod.FileName;

                if (!string.IsNullOrEmpty(fileName))
                    prod.Base64 = new DocumentBpl(_cache, _tenant).GetBase64(GlobalUpload.DocumentType.PRODUCTIMAGE, fileName);
            }

            return ResultWrap(products, "Product");
        }

        public Result GetProduct(int productId)
        {
            var result = new ProductRal(_tenant).GetProductById(productId);

            result.Base64 = new DocumentBpl(_cache, _tenant).GetBase64(GlobalUpload.DocumentType.PRODUCTIMAGE, result.FileName);

            return ResultWrap(result, "Product");
        }

        public Result DeleteProduct(int productId, string fileName = null)
        {
            bool result = new ProductRal(_tenant).DeleteProduct(productId);

            if (result && !string.IsNullOrEmpty(fileName))
            {
                new DocumentBpl(_cache, _tenant).DeleteFile(GlobalUpload.DocumentType.PRODUCTIMAGE, fileName);
            }

            return ResultWrap(result, "Result");
        }

        public (string, string, string) UploadImage(string base64, string fileName)
        {
            string thumbFileName = string.Empty;
            string error = string.Empty;
            if (!string.IsNullOrEmpty(base64) && !string.IsNullOrEmpty(fileName))
            {
                var documentBpl = new DocumentBpl(_cache, _tenant);
                error = documentBpl.ValidateFileFormat(GlobalUpload.DocumentType.PRODUCTIMAGE, fileName);

                if (error != string.Empty)
                    return (error, fileName, thumbFileName);
                (fileName) = documentBpl.Upload(GlobalUpload.DocumentType.PRODUCTIMAGE, base64, fileName);
                if (fileName == string.Empty)
                {
                    return (error, string.Empty, string.Empty);
                }
                else
                {
                    try
                    {
                        thumbFileName = documentBpl.SaveThumbnail(GlobalUpload.DocumentType.PRODUCTIMAGE, _tenant.ImageThumbWidth, _tenant.ImageThumbHeight, base64, fileName);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return (error, fileName, thumbFileName);
        }

    }
}
