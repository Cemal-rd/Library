using Library.Context;
using Library.Models;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public BookController(AppDbContext appDbContext, IWebHostEnvironment hostingEnvironment) 
        {
            _hostingEnvironment= hostingEnvironment;
            _appDbContext= appDbContext;
        }
        public IActionResult Index()
        {
            //kitaplar ismine göre alfatik şekilde listelenir
            var books=_appDbContext.Books.OrderBy(x => x.Title).ToList();
            return View(books);
        }
        public IActionResult Borrow(int id)
        {
            var book = _appDbContext.Books.Find(id);
            if (book != null && book.IsAvailable)
            {
                // Eylemin yapıldığı anın zamanını al
                DateTime borrowDate = DateTime.Now;

                // Borrow view'ına kitap bilgilerini taşı
                return View("Borrow", book);
            }
            else
            {
                TempData["ErrorMessage"] = "Book is not available for borrowing.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Borrow(Book model)
        {
            // Ödünç alınacak kitabın güncellenmiş bilgilerini al
            var book = _appDbContext.Books.Find(model.Id);

            // Ödünç alınan kitabın bilgilerini güncelle
            if (book != null)
            {
                // ReturnDate bilgisini kontrol et
                if (model.ReturnDate != null && model.ReturnDate > DateTime.Now)
                {
                    // Ödünç alma işlemi başarılı
                    book.IsAvailable = false;
                    book.Borrower = "Borrower Name";
                    book.BorrowDate = DateTime.Now;
                    book.ReturnDate = model.ReturnDate;

                    // Değişiklikleri kaydet
                    _appDbContext.SaveChanges();

                    TempData["SuccessMessage"] = "Book borrowed successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    // Geçersiz iade tarihi
                    TempData["ErrorMessage"] = "Invalid return date. Please enter a future date.";
                    return View(model);
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Book information not found.";
                return RedirectToAction("Index");
            }
        }

        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(Book model, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Resim dosyasını yükleme
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    // Book nesnesini oluştur ve veritabanına ekle
                    var book = new Book
                    {
                        Title = model.Title,
                        Author = model.Author,
                        ImageUrl =  uniqueFileName, // Resmin URL'si
                        IsAvailable = true
                    };

                    _appDbContext.Books.Add(book);
                    _appDbContext.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please select a file to upload.");
                }
            }

            return View(model);
        }
    }

    
}
