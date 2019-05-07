using src.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebServices.Controllers
{
    public class StoreController : Controller
    {
        ServiceLayer service = ServiceLayer.getInstance();

        [Route("api/store/CreateProductInStore")]
        [HttpGet]

        public string createProductInStore(string userName, string productName, string category, string detail, int producrPrice, string storeName)
        {

            bool ans = service.createNewProductInStore(productName, category, detail, producrPrice, storeName, userName);
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
        public string addProductInStore(string userName, string productName, int productQuantity, string storeName)
        {
            List<KeyValuePair<String, int>> productList = new List<KeyValuePair<String, int>>();
            productList.Add(new KeyValuePair<String, int>(productName, productQuantity));

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
        public string removeProductInStore(string userName, string productName, int productQuantity, string storeName)
        {
            List<KeyValuePair<String, int>> productList = new List<KeyValuePair<String, int>>();
            productList.Add(new KeyValuePair<String, int>(productName, productQuantity));

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


    }
}