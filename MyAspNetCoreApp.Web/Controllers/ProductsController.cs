using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.FileProviders;
using MyAspNetCoreApp.Web.Filters;
using MyAspNetCoreApp.Web.Helpers;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModel;
using Newtonsoft.Json.Linq;

namespace MyAspNetCoreApp.Web.Controllers
{
    [LogFilter]
    public class ProductsController : Controller
    {
        private readonly IMapper _mapper;
        private AppDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly ProductRepository _productRepository;
        //private readonly ProductService _productService;
        /*ProductService productService*/
        public ProductsController(AppDbContext context, IMapper mapper)
        {
            //DI Conatiner
            //Dependency Injection Pattern

            _productRepository = new ProductRepository();

            _context = context;
            _mapper = mapper;


            //_productService = productService;


            //Linq metodu
            //if (!_context.Products.Any())
            //{
            //    _context.Products.Add(new Product() { Name = "Bilgisayar", Price = 32000, Stock = 25, Color = "Red" /*,Height = 5, Width = 8*/ });
            //    _context.Products.Add(new Product() { Name = "Klavye", Price = 300, Stock = 125, Color = "Blue"/*, Height = 9, Width = 4 */});
            //    _context.Products.Add(new Product() { Name = "Mouse", Price = 150, Stock = 225, Color = "Pink"/*, Height = 7, Width = 6 */});


            //    _context.SaveChanges();
            //}


        }

        public IActionResult Index()
        {
            var text = "Asp.Net";


            var products = _context.Products.Where(x => !x.IsDeleted).ToList();
            return View(_mapper.Map<List<ProductViewModel>>(products));
        }

        [Route("[controller]/[action]/{page}/{pagesize}")]
        public IActionResult Pages(int page, int pageSize)
        {
            var products = _context.Products.Skip((page - 1) * (pageSize)).Take(pageSize).ToList();
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;

            return View(_mapper.Map<List<ProductViewModel>>(products));
        }


        //[Route("[controller]/[action]/{productid}")]
        [ServiceFilter(typeof(NotFoundFilter))]
        [Route("urunler/urun/{productid}")]
        public IActionResult GetById(int productid)
        {
            var products = _context.Products.Find(productid);
            return View(_mapper.Map<ProductViewModel>(products));
        }

        public IActionResult Remove2(int id)
        {
            var product = _context.Products.Find(id);

            _context.Products.Remove(product);

            //  _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            product.IsDeleted = true;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private IActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Expire = new Dictionary<string, int>()
            {
                  {"1 Ay" ,1 },
                  {"3 Ay" ,3 },
                  {"6 Ay" ,6 },
                  {"12 Ay" ,12 }
            };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>() {

            new () {Data="Mavi" ,Value="Mavi"},
            new () {Data="Kirmizi" ,Value="Kirmizi"},
            new () {Data="Sari" ,Value="Sari"}
            }, "Value", "Data");


            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel newProduct)
        {
            IActionResult result = null;

            if (ModelState.IsValid)
            {
                try
                {

                    var root = _fileProvider.GetDirectoryContents("wwwroot");

                    var images = root.FirstOrDefault(x => x.Name == "images");

                    if (images != null)
                    {
                        var path = Path.Combine(images.PhysicalPath, newProduct.Image.FileName);

                        using var stream = new FileStream(path, FileMode.Create);

                        newProduct.Image.CopyTo(stream);


                        var product = _mapper.Map<Product>(newProduct);
                        product.ImagePath = newProduct.Image.FileName;


                        _context.Products.Add(product);
                        _context.SaveChanges();
                        TempData["status"] = "Urun basariyla eklendi. ";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["status"] = "Error: 'images' directory not found.";
                        result = View();
                    }

                }
                catch (Exception)
                {
                    result = View();
                }
            }
            else
            {
                result = View();
            }

            ViewBag.Expire = new Dictionary<string, int>()
            {
                  {"1 Ay" ,1 },
                  {"3 Ay" ,3 },
                  {"6 Ay" ,6 },
                  {"12 Ay" ,12 }
            };
            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>() {

            new () {Data="Mavi" ,Value="Mavi"},
            new () {Data="Kirmizi" ,Value="Kirmizi"},
            new () {Data="Sari" ,Value="Sari"}
            }, "Value", "Data");

            return result;
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);

            ViewBag.ExpireValue = product.Expire;
            ViewBag.Expire = new Dictionary<string, int>()
            {
                  {"1 Ay" ,1 },
                  {"3 Ay" ,3 },
                  {"6 Ay" ,6 },
                  {"12 Ay" ,12 }
            };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>() {

            new () {Data="Mavi" ,Value="Mavi"},
            new () {Data="Kirmizi" ,Value="Kirmizi"},
            new () {Data="Sari" ,Value="Sari"}
            }, "Value", "Data", product.Color);

            return View(_mapper.Map<ProductViewModel>(product));
        }
        [HttpPost]
        public IActionResult Update(ProductViewModel updateProduct)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.ExpireValue = updateProduct.Expire;
                ViewBag.Expire = new Dictionary<string, int>()
               {
                  {"1 Ay" ,1 },
                  {"3 Ay" ,3 },
                  {"6 Ay" ,6 },
                  {"12 Ay" ,12 }
               };

                ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>() {

                   new () {Data="Mavi" ,Value="Mavi"},
                   new () {Data="Kirmizi" ,Value="Kirmizi"},
                   new () {Data="Sari" ,Value="Sari"}
                 }, "Value", "Data", updateProduct.Color);

                return View();
            }
            _context.Products.Update(_mapper.Map<Product>(updateProduct));
            _context.SaveChanges();

            TempData["status"] = "Urun basariyla guncellendi. ";

            return RedirectToAction("Index");
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult HasProductName(string Name)
        {
            var anyProduct = _context.Products.Any(x => x.Name.ToLower() == Name.ToLower());

            if (anyProduct)
            {
                return Json("There is already a product in this name. ");
            }
            else
            {
                return Json(true);
            }
        }
    }
}
