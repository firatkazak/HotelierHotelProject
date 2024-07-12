using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Uygulaman�n kimlik do�rulama hizmetine JWT tabanl� kimlik do�rulama �emas�n� ekler. 
    .AddJwtBearer(opt =>//Bu, JWT tabanl� kimlik do�rulama i�in yap�land�rma se�eneklerini belirtmek i�in bir lambda ifadesi ba�lat�r.
    {
        opt.RequireHttpsMetadata = false;//Bu, HTTPS �zerinden ileti�im zorunlulu�unu devre d��� b�rak�r. True yaparsak etkinle�tirir.
        opt.TokenValidationParameters = new TokenValidationParameters()//JWT'nin ge�erlili�ini do�rulamak i�in kullan�lacak parametreleri belirler.
        {
            ValidIssuer = "http://localhost",//Token'�n ge�erli oldu�u yay�nc� (issuer) URL'sini belirtir. Burada "http://localhost" olarak ayarlanm��.
            ValidAudience = "http://localhost",//Token'�n ge�erli oldu�u hedef (audience) URL'sini belirtir. Burada da "http://localhost" olarak ayarlanm��.
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aspnetcoreapiapi")),//Burada "aspnetcoreapiapi" olarak ayarlanm��.
            //Token'�n imzalanm�� olup olmad���n� do�rulamak i�in kullan�lacak simetrik anahtar� belirtir. Bu anahtar, JWT'nin olu�turuldu�u anahtarla ayn� olmal�d�r.
            ValidateIssuerSigningKey = true,//Bu, yay�nc� imza anahtar�n�n do�rulama i�lemlerine kat�l�p kat�lmayaca��n� belirtir.
            //true olarak ayarland���ndan, anahtar�n do�rulama s�re�lerine dahil edilecektir. false olsayd�, dahil edilmeyecekti.
            ValidateLifetime = true,//Token'�n ge�erlilik s�resinin kontrol edilip edilmeyece�ini belirtir. 
            ClockSkew = TimeSpan.Zero//Token'�n ge�erlilik s�resi kontrol� s�ras�nda izin verilen saat fark�n� belirtir.
            //TimeSpan.Zero olarak ayarland���ndan, hi�bir saat fark�na izin verilmeyecek ve token'�n tam olarak ge�erlilik s�resinde olmas� gerekecektir.
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
