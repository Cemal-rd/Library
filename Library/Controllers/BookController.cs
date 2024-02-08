using Library.Context;
using Library.Models;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Library.Helpers;
using System.Xml.Linq;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly Logger _logger;

        public BookController(AppDbContext appDbContext, IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _appDbContext = appDbContext;
            _logger = new Logger("book_controller_log.txt");
        }

        public IActionResult Index()
        {
            LogAction("Index");
            try
            {
                //kitaplar ismine göre alfatik şekilde listelenir
                var books = _appDbContext.Books.OrderBy(x => x.Title).ToList();
                return View(books);
            }
            catch (Exception ex)
            {
                LogError("Error in Index method", ex);
                throw;
            }
        }

        public IActionResult Borrow(int id)
        {
            LogAction("Borrow");
            try
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
            catch (Exception ex)
            {
                LogError("Error in Borrow method", ex);
                throw;
            }
        }

        [HttpPost]
        public IActionResult Borrow(Book model)
        {
            LogAction("Borrow [POST]");
            try
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
                        book.Borrower = model.Borrower;
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
            catch (Exception ex)
            {
                LogError("Error in Borrow [POST] method", ex);
                throw;
            }
        }

        public IActionResult AddBook()
        {
            LogAction("AddBook");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(Book model, IFormFile imageFile)
        {
            LogAction("AddBook [POST]");
            try
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
                            ImageUrl = uniqueFileName, // Resmin URL'si
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
            catch (Exception ex)
            {
                LogError("Error in AddBook [POST] method", ex);
                throw;
            }
        }
        //log işlemleri
        private void LogAction(string actionName)
        {
            _logger.Log($"Executing action: {actionName}");
        }

        private void LogError(string message, Exception ex)
        {
            _logger.Log($"ERROR: {message}. Exception: {ex.Message}");
        }
    }
}
