# Ocdata.Operations
> Project For Common Data Operations (.NET 6.0)
> Nuget Package : Install-Package Ocdata.Operations -Version 1.1.7

'write-your-code-here'


You will need the following tools:

* [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
* [.Net Core 5 or later](https://dotnet.microsoft.com/download/dotnet-core/5)
* [Docker Desktop](https://www.docker.com/products/docker-desktop)

## Installing

1) Öncelikle bilgisayarda Docker Desktop kurulu olması gerekiyor.

2) Projeyi indirdikten sonra docker-compose projesine sağ tıklayıp open in terminal demeniz gerekiyor.

3) Aşağıdaki komutu çalıştırdığınızda veritabanları pgadmin ve rabbitmq docker üzerinde kurulmuş olur.
```csharp
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```
4) Catalog.Infrastructure projesinde Update-Database komutu çalıştırılmalıdır (Migration'lar hazır).

5) PgAdmin'e erişmek için http://localhost:5050 login ekranı
   
   Kullanıcı Adı = ozcan.caparoglu@gmail.com
   
   Şifre = admin1234

6) PgAdmin'e giriş yapıldıktan sonra *Add New Server -> General Name = contactdb Connection > Host = contactdb* 

   Username = admin Password = admin1234

## Notes

Api projelerini docker üzerinde çalıştırmadım;

**Contact.Api** => Could not load type 'Npgsql.TypeMapping.NpgsqlTypeMapping' from assembly 'Npgsql, Version=6.0.2.0 böyle bir hata veriyor.
 
Baştan sildim NugetPackage'ları düzelttim hatta solution'ı baştan create ettim .NET 6.0 framework ve sdk'lar karıştığından dolayı olabilir.

https://github.com/elsa-workflows/elsa-core/issues/2544

**Report.Api** => Unable to resolve service for type 'Report.Api.Data.Interfaces.IReportContext' hatası veriyor. Scope yapıldığı halde docker üzerinde hata
vermeye devam ediyor. (Docker üzerinde neden çalışmıyor hiç anlamadım.)

Her ikisini de visual studio üzerinden çalıştırarak denedim.

Eğer api'lar docker üzerinde çalışırsa çektiğinizde

**Contact.Api** => http://localhost:8000/swagger/index

**Report.Api** => http://localhost:8001/swagger/index

Bu durumda Report.Api'daki appsettings'de ContactBaseUrl'i http://localhost:8000 olarak değiştirmeniz gerekiyor.
