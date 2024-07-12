using FluentValidation;
using FluentValidation.AspNetCore;
using HotelierProject.DataAccessLayer.Concrete;
using HotelierProject.EntityLayer.Concrete;
using HotelierProject.WebUI.Dtos.GuestDto;
using HotelierProject.WebUI.ValidationRules.GuestValidationRules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>();//Context s�n�f�m�z� tan�tt�k.
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>();//Identity ayar�.
builder.Services.AddHttpClient();//Http istemcisi i�in ekledik.
builder.Services.AddTransient<IValidator<CreateGuestDto>, CreateGuestValidator>();
builder.Services.AddTransient<IValidator<UpdateGuestDto>, UpdateGuestValidator>();
builder.Services.AddControllersWithViews();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMvc(config =>
{
    AuthorizationPolicy policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();//Otantike olmu� bir kullan�c� gereksinimine sahip, bir policy in�a et.
    config.Filters.Add(new AuthorizeFilter(policy));//Yukar�da in�a etti�imiz policy'i burada kullan�yoruz.
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;//Sadece Http'ye izin ver.
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);//Login kalma s�resi biz burada 10 dk verdik. Facebook gibi sitelerde 6 ay bu s�re.
    options.LoginPath = "/Login/Index/";//Kullanabilmek i�in Login olmam�z gereken sayfalara gitmek istedi�imizde bizi Default olarak g�nderece�i sayfa.
    //NOT: Oda ekleme vs i�lemleri yapmak i�in Login olunmal�, Fakat ana sayfa, login sayfas� mant�ken login olmadan girilebilmeli.
    //��te bu sayfalara eri�mek i�in o sayfan�n Controller'�na AllowAnonymous attribute'unu atamal�y�z.
});

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404", "?code={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Default}/{action=Index}/{id?}");
app.Run();
