using FluentValidation;
using FluentValidation.AspNetCore;
using HotelierProject.DataAccessLayer.Concrete;
using HotelierProject.EntityLayer.Concrete;
using HotelierProject.WebUI.Dtos.GuestDto;
using HotelierProject.WebUI.ValidationRules.GuestValidationRules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>();//Context sýnýfýmýzý tanýttýk.
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>();//Identity ayarý.
builder.Services.AddHttpClient();//Http istemcisi için ekledik.
builder.Services.AddTransient<IValidator<CreateGuestDto>, CreateGuestValidator>();
builder.Services.AddTransient<IValidator<UpdateGuestDto>, UpdateGuestValidator>();
builder.Services.AddControllersWithViews();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMvc(config =>
{
    AuthorizationPolicy policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();//Otantike olmuþ bir kullanýcý gereksinimine sahip, bir policy inþa et.
    config.Filters.Add(new AuthorizeFilter(policy));//Yukarýda inþa ettiðimiz policy'i burada kullanýyoruz.
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;//Sadece Http'ye izin ver.
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);//Login kalma süresi biz burada 10 dk verdik. Facebook gibi sitelerde 6 ay bu süre.
    options.LoginPath = "/Login/Index/";//Kullanabilmek için Login olmamýz gereken sayfalara gitmek istediðimizde bizi Default olarak göndereceði sayfa.
    //NOT: Oda ekleme vs iþlemleri yapmak için Login olunmalý, Fakat ana sayfa, login sayfasý mantýken login olmadan girilebilmeli.
    //Ýþte bu sayfalara eriþmek için o sayfanýn Controller'ýna AllowAnonymous attribute'unu atamalýyýz.
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
