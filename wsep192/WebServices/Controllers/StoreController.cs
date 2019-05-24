using src.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace WebServices.Controllers
{
    public class StoreController : ApiController
    {
        ServiceLayer service = ServiceLayer.getInstance();

        [Route("api/store/CreateProductInStore")]
        [HttpGet]
        public string createProductInStore(string userName, string productName, string category, string detail, string productPrice, string storeName)
        {
            int price;
            try
            {
                price = Int32.Parse(productPrice);
            }
            catch (Exception e)
            {
                return "The price of the product shuld be number";
            }
            bool ans = service.createNewProductInStore(productName, category, detail, price , storeName, userName);
            switch (ans)
            {
                case true:
                    return "Product successfully created";
                case false:
                    return "Error in create product";
            }
            return "server error: createProductInStore";
        }

        [Route("api/store/AddProductInStore")]
        [HttpGet]
        public string addProductInStore(string userName, string productName, string productQuantity, string storeName)
        {
            int quantity;
            try
            {
                quantity = Int32.Parse(productQuantity);
            }
            catch (Exception e)
            {
                return "The quantity of the product shuld be number";
            }
            List<KeyValuePair<String, int>> productList = new List<KeyValuePair<String, int>>();
            productList.Add(new KeyValuePair<String, int>(productName, quantity));

            bool ans = service.addProductsInStore(productList, storeName, userName);
            switch (ans)
            {
                case true:
                    return "Product successfully added to store";
                case false:
                    return "Error in add product in store";
            }
            return "server error: AddProductInStore";
        }

        [Route("api/store/RemoveProductInStore")]
        [HttpGet]
        public string removeProductInStore(string userName, string productName, string productQuantity, string storeName)
        {
            int quantity;
            try
            {
                quantity = Int32.Parse(productQuantity);
            }
            catch (Exception e)
            {
                return "The quantity of the product shuld be number";
            }
            List<KeyValuePair<String, int>> productList = new List<KeyValuePair<String, int>>();
            productList.Add(new KeyValuePair<String, int>(productName, quantity));

            bool ans = service.removeProductsInStore(productList, storeName, userName);
            switch (ans)
            {
                case true:
                    return "Product successfully removed from store";
                case false:
                    return "Error in remove product in store";
            }
            return "server error: RemoveProductInStore";
        }

        [Route("api/store/EditProductInStore")]
        [HttpGet]
        public string editProductInStore(string userName, string productName, string newProductName, string category, string detail, string productPrice, string storeName)
        {
            int price;
            try
            {
                price = Int32.Parse(productPrice);
            }
            catch (Exception e)
            {
                return "The price of the product shuld be number";
            }

            bool ans = service.editProductInStore(productName, newProductName, category, detail, price, storeName, userName);
            switch (ans)
            {
                case true:
                    return "Product successfully edited in store";
                case false:
                    return "Error in edit product in store";
            }
            return "server error: EditProductInStore";
        }


    }
}