using Microsoft.AspNetCore.Mvc;
using ProjectPrn211.Models;
using Microsoft.AspNetCore.Http;

namespace ProjectPrn211.Controllers
{
    public class HomeController : Controller
    {
        MyOrderContext context = new MyOrderContext();
        public IActionResult Index(int id)
        {

             
            var listProducts = context.Products.ToList();
            
            if(id != 0)
            {
                listProducts = context.Products.Where(product => product.CategoryId == id).ToList();
                Category category = context.Categories.FirstOrDefault(cate => cate.CategoryId == id);
                ViewBag.category = category;
                ViewBag.selecedId = id; 
            }  
            
            
            var listCategories = context.Categories.ToList();

            
            ViewBag.listCategories = listCategories;
            return View(listProducts);
        }

        public IActionResult ViewCart()
        {
            var product = context.Products.ToList();
            ViewBag.lorder = lorder;
            
            return View(product);
        }

        public static List<OrderLine> lorder = new List<OrderLine>();

        public IActionResult AddCart(int id)
        {
            
               
                Product p = context.Products.FirstOrDefault(pr => pr.ProductId == id);
                
                if (p.UnitInStoke > 0)
                {
                    addOrder(id, 1);
                   
                    p.UnitInStoke--;
                context.SaveChanges();
                }
               
                
                return RedirectToAction("Index");

            
        }

        

        public IActionResult RemoveOrderLine(int id)
        {
            OrderLine o = lorder.FirstOrDefault(order => order.ProductId == id);
            lorder.Remove(o); 
            Product product = context.Products.FirstOrDefault(pro => pro.ProductId == id);
            product.UnitInStoke += o.Quantity;
            context.SaveChanges();
              
            return RedirectToAction("ViewCart");
        }

        public IActionResult PlaceOrder()
        {
            Account acc = new Account();
            return View(acc);
        }
        [HttpPost]
        public IActionResult PlaceOrder(Account acc)
        {
            context.Accounts.Add(acc); 
            context.SaveChanges();
            int id = context.Accounts.OrderBy(acc => acc.AccountId).LastOrDefault().AccountId;
           
            foreach (OrderLine o in lorder)
            {
                o.AccountId = id;
                context.OrderLines.Add(o);
                context.SaveChanges();
                
            }
            lorder.Clear();
            return RedirectToAction("Index");
           
        }

        
        public IActionResult ViewDetail(int id)
        {
            using (MyOrderContext context = new MyOrderContext())
            {
                Product product = context.Products.FirstOrDefault(p => p.ProductId == id);
                var listCategories = context.Categories.ToList();

                ViewBag.listCategories = listCategories;
                return View(product);
            }
        }

        [HttpPost]
        public IActionResult Search(string ProductName)
        {
            using (MyOrderContext context = new MyOrderContext())
            {
                var listProducts = context.Products.ToList();
                if (ProductName != null)
                {
                  listProducts = context.Products.Where(p => p.ProductName.Contains(ProductName)).ToList();

                }
                var listCategories = context.Categories.ToList();


                ViewBag.listCategories = listCategories;
                return View("Index", listProducts);
            }
        }
        public IActionResult Admin()
        {
           
            return View();  
        }
        [HttpPost]
        public IActionResult Admin(string username, string password)
        {
            
            if(username!="admin" || password!="admin")
            {
                ViewBag.mess = "Your Username or Password is invalid";
                return View("Admin");
            }
           
            return RedirectToAction("manageProduct");
        }

        public IActionResult manageProduct()
        {
            var listProducts = context.Products.ToList();
            var listCategory = context.Categories.ToList();
            ViewBag.listCategory = listCategory;
            return View("ManageProduct", listProducts);
        }

        public IActionResult Add()
        {
            var selectCate = context.Categories.ToList();
            ViewBag.selectCate = selectCate;
            Product p = new Product();

            return View("AddProduct",p);
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            if(product != null)
            {
            context.Products.Add(product);
            if (context.SaveChanges() > 0)
            {
                ViewBag.notice = "ADD SUCCESS FULL";
            }

            }



            return RedirectToAction("manageProduct");

        }

        public IActionResult DeleteProduct(int id)
        {
            using (MyOrderContext context = new MyOrderContext())
            {


                Product product = context.Products.FirstOrDefault(p => p.ProductId == id);
                if (product != null)
                {
                    if (context.OrderLines.FirstOrDefault(o => o.ProductId == id) != null)
                    {
                    ViewBag.notice = "Can't Remove! Item in Process to shipping...";
                    }
                    else
                    {
                        context.Products.Remove(product);
                        context.SaveChanges();
                    }
                }    
                
                return RedirectToAction("manageProduct");
            }
        }

        public IActionResult UpdateProduct(int id)
        {
            using(MyOrderContext context = new MyOrderContext())
            {
                var product = context.Products.FirstOrDefault(p => p.ProductId == id);
                if (product != null)
                {
                    var selectCate = context.Categories.ToList();
                    ViewBag.selectCate = selectCate;
                    
                }
                return View("UpdateProduct", product);
            }   
        }
        [HttpPost] 
        public IActionResult UpdateProduct(Product product)
        {
            using(MyOrderContext context = new MyOrderContext())
            {
                var p = context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                p.ProductName = product.ProductName;
                p.Image = product.Image;
                p.CategoryId = product.CategoryId;
                p.UnitInStoke = product.UnitInStoke;
                p.UnitPrice = product.UnitPrice;
                context.SaveChanges();
                return RedirectToAction("manageProduct");
            }
        }

        
        public IActionResult ViewOrder(string email, string phone)
        {
            var listorder = context.OrderLines.Where(l => l.Account.Email == email && l.Account.PhoneNumber == phone).ToList();
            var lproduct = context.Products.ToList();
            ViewBag.lproduct = lproduct;

            ViewBag.status = "packing";


            ViewBag.email = email;
            ViewBag.phone = phone;  
            return View("ViewOrder",listorder);
        }

        public IActionResult CannelOrder(int id,string email, string phone)
        {
            using(MyOrderContext context = new MyOrderContext())
            {
                OrderLine o = context.OrderLines.FirstOrDefault(or => or.OrderLineId == id);
                Product p = context.Products.FirstOrDefault(product => product.ProductId == o.ProductId);
                p.UnitInStoke += o.Quantity;
                context.OrderLines.Remove(o);
                context.SaveChanges();

                var listorder = context.OrderLines.Where(l => l.Account.Email == email && l.Account.PhoneNumber == phone).ToList();
                var lproduct = context.Products.ToList();
                ViewBag.lproduct = lproduct;
                ViewBag.email = email;
                ViewBag.phone = phone;
                return View("ViewOrder", listorder);
            }
        }


        public OrderLine GetOrderLine(int id)
        {
            OrderLine or = lorder.FirstOrDefault(or => or.ProductId == id); 
            return or; 
            
        }

        public void addOrder(int id,int uid )
        {
            if(GetOrderLine(id) != null)
            {
                OrderLine o = GetOrderLine(id);
                lorder.Remove(o);
                o.Quantity += 1; 
                lorder.Add(o);  
            }
            else
            {
                
                lorder.Add(new OrderLine()  { AccountId=uid,ProductId=id,Quantity=1 });
            }
           
        }

        
    }
}
