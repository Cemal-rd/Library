# Library
Bu proje, bir kütüphane yönetim uygulamasıdır. Kullanıcılar, kütüphanede bulunan kitapları listeleme, ödünç almave yeni kitap ekleme gibi işlemleri yapabilirler. Ayrıca, helpers içine yazdığım logger sınıfı sayesinde yapılan tüm işlemler .txt dosyasına loglanıyor.
<h1>Projede Kullanılan Teknolojiler</h1>
- .Net Core MVC
- MSSQL
-Entity Framework Code First
<h2>Kurulum</h2>
<h3>Bu depoyu yerel makinenize klonlayın: git clone https://github.com/Cemal-rd/Library.git</h3>
<h3>appsettings.json dosyasındaki "DefaultConnection" bağlantı dizesini güncelleyin.</h3>
<h3>Veritabanını oluşturun </h3>
-dotnet ef database update
<h1>Projeyi Çalıştırın:</h1>
-dotnet run
<h2>Kullanım</h2>
<h3>-Ana sayfada kütüphanede bulunan tüm kitapları görüntüleyebilirsiniz.</h3>
<h3>-"Borrow" düğmesine tıklayarak bir kitabı ödünç alabilirsiniz.</h3>
<h3>-"Add New Book" düğmesine tıklayarak kütüphaneye yeni bir kitap ekleyebilirsiniz.</h3>
<h1>Uygulama Önyüzü</h1>
![Library2](https://github.com/Cemal-rd/Library/assets/107768783/b9c6aef2-7942-45e4-a211-a801d61a17e8)
Anasayfa böyle gözükmekte
![Library1](https://github.com/Cemal-rd/Library/assets/107768783/bd865177-03f2-4960-bd3d-8fbefa309bf5)
AddBook sayfası bu şekilde kitap ismi yazar ve resmi ile kitap eklenebilmekte.
![Library3](https://github.com/Cemal-rd/Library/assets/107768783/22cb5167-3641-433c-a3e0-aee1b2af3942)
ödünç alma kısmında ise tarih ve ödünç alan ismi ile ödünç alınabilmekte.




