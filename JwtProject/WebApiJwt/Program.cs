using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Uygulamanýn kimlik doðrulama hizmetine JWT tabanlý kimlik doðrulama þemasýný ekler. 
    .AddJwtBearer(opt =>//Bu, JWT tabanlý kimlik doðrulama için yapýlandýrma seçeneklerini belirtmek için bir lambda ifadesi baþlatýr.
    {
        opt.RequireHttpsMetadata = false;//Bu, HTTPS üzerinden iletiþim zorunluluðunu devre dýþý býrakýr. True yaparsak etkinleþtirir.
        opt.TokenValidationParameters = new TokenValidationParameters()//JWT'nin geçerliliðini doðrulamak için kullanýlacak parametreleri belirler.
        {
            ValidIssuer = "http://localhost",//Token'ýn geçerli olduðu yayýncý (issuer) URL'sini belirtir. Burada "http://localhost" olarak ayarlanmýþ.
            ValidAudience = "http://localhost",//Token'ýn geçerli olduðu hedef (audience) URL'sini belirtir. Burada da "http://localhost" olarak ayarlanmýþ.
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aspnetcoreapiapi")),//Burada "aspnetcoreapiapi" olarak ayarlanmýþ.
            //Token'ýn imzalanmýþ olup olmadýðýný doðrulamak için kullanýlacak simetrik anahtarý belirtir. Bu anahtar, JWT'nin oluþturulduðu anahtarla ayný olmalýdýr.
            ValidateIssuerSigningKey = true,//Bu, yayýncý imza anahtarýnýn doðrulama iþlemlerine katýlýp katýlmayacaðýný belirtir.
            //true olarak ayarlandýðýndan, anahtarýn doðrulama süreçlerine dahil edilecektir. false olsaydý, dahil edilmeyecekti.
            ValidateLifetime = true,//Token'ýn geçerlilik süresinin kontrol edilip edilmeyeceðini belirtir. 
            ClockSkew = TimeSpan.Zero//Token'ýn geçerlilik süresi kontrolü sýrasýnda izin verilen saat farkýný belirtir.
            //TimeSpan.Zero olarak ayarlandýðýndan, hiçbir saat farkýna izin verilmeyecek ve token'ýn tam olarak geçerlilik süresinde olmasý gerekecektir.
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
