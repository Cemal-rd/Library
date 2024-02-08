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
![Library2](https://github.com/Cemal-rd/Library/assets/107768783/ecd42373-1405-4754-9c6d-bbce8b2f2ad7)
Uygulama Anasayfası bu şekilde.Sadece kütüphanede olan kitapların yanında ödünç alma butonu açık.Dışarıda olan kitapların ne zaman döneceği ve kimin elinde olduğu gözüküyor.
![Library1](https://github.com/Cemal-rd/Library/assets/107768783/9ac4ecc0-11e5-4bf7-8ad8-7bdeacbba925)
Kitap Ekleme kısmıda bu şekilde kitabın ismi, yazar ismi ve kitap resmi ile kitap eklenebilmekte.
![Library3](https://github.com/Cemal-rd/Library/assets/107768783/50af1dd6-cb87-4d63-b105-b4d5ef7bd142)
Ödünç alma kısmında ise isim ve geri getirme tarihi ile kitap ödünç alabilmektesiniz.





