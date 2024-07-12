using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApiJwt.Models;

public class CreateToken
{
    public string TokenCreate()
    {
        byte[] bytes = Encoding.UTF8.GetBytes("aspnetcoreapiapi");//Program.cs'de oluşturduğumuz Key'i burada geçiyoruz.

        SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);//Simetrik Güvenlik Key'i nesnesi örnekledik. Bu bytes'da oluşturduğumuz key'i veriyoruz.

        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);//HmacSha256: Şifreleme Algoritması.
        //SigningCredentials: Dijital imza oluşturmak için kullanılan şifreleme anahtarını ve güvenlik algoritmalarını temsil eder.

        JwtSecurityToken token = new JwtSecurityToken
            (
            issuer: "http://localhost",//issuer:üretici,
            audience: "http://localhost",//audience:tüketici,
            notBefore: DateTime.Now,//JWT'nin anında oluşturulduğunda bile, geçerlilik süresi başlamadan önce kullanılamayacağı anlamına gelir.
            expires: DateTime.Now.AddMinutes(3),//expires: Token'in geçerlilik süresi.
            signingCredentials: credentials//signingCredentials: Yukarıda tanımladığımız credentials
            );

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();//TokenHandler oluşturduk.
        return handler.WriteToken(token);//handler aracılığıyla, token'dan gelen değeri WriteToken metodu ile Token'a çevirecek.
    }

    public string TokenCreateAdmin()
    {
        byte[] bytes = Encoding.UTF8.GetBytes("aspnetcoreapiapi");//Program.cs'de oluşturduğumuz Key'i burada geçiyoruz.

        SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);//Simetrik Güvenlik Key'i nesnesi örnekledik. Bu bytes'da oluşturduğumuz key'i veriyoruz.

        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);//HmacSha256: Şifreleme Algoritması.

        List<Claim> claimList = new List<Claim>()//Claim Nesnesi rollerimizin içeriğini tutmamıza yarıyor.
        {
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),//Guid.NewGuid(): Yeni bir benzersiz GUID oluşturur
            //NameIdentifier: Bir kullanıcının benzersiz kimlik bilgisini temsil eden bir öntanımlı talep türüdür. Genellikle kullanıcı ID'sini içerir.
            //Özet: Bu iki ifadeyi birleştirerek, her yeni token oluşturulduğunda kullanıcının benzersiz bir kimlik bilgisine sahip olmasını sağlarız.
            new Claim(ClaimTypes.Role, "Admin"),//Rol: Admin
            new Claim(ClaimTypes.Role, "Visitor"),//Rol: Visitor
        };

        JwtSecurityToken token = new JwtSecurityToken
            (//Üstteki kodun aynısı. Ekstra olan: claims
            issuer: "http://localhost",
            audience: "http://localhost",
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddSeconds(30),
            signingCredentials: credentials,
            claims: claimList//claims: tanımladığımız roller listesini veriyoruz burada.
            );

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();//TokenHandler oluşturduk.

        return handler.WriteToken(token);//handler aracılığıyla, token'dan gelen değeri WriteToken metodu ile Token'a çevirecek.
    }
}

