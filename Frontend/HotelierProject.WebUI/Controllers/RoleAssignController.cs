using HotelierProject.EntityLayer.Concrete;
using HotelierProject.WebUI.Models.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelierProject.WebUI.Controllers
{
    public class RoleAssignController : Controller
    {
        private readonly UserManager<AppUser> _userManager;//Kullanıcıya rol atamak için bir tane UserManager'dan Örnek aldık.
        private readonly RoleManager<AppRole> _roleManager;//Kullanıcıya rol atamak için bir tane RoleManager'dan Örnek aldık.
        //Daha sonra Constructor geçtik.
        public RoleAssignController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var values = _userManager.Users.ToList();//userManager'daki User'ların Listesini ver.
            return View(values);//Verdiğin Listeyi dön.
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(int id)//Kullanıcının ID'sine göre atama işlemi yapacağız.
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);//Kullanıcı ID'si verilen ID'ye eşit ise değeri user'a atacak.
            TempData["userid"] = user.Id;//Kullanıcının ID'sini yakalamış olduk.
            var roles = _roleManager.Roles.ToList();//RoleManager'daki Rolleri listeliyoruz.
            var userRoles = await _userManager.GetRolesAsync(user);//Rolleri user için getiriyoruz.
            List<RoleAssignViewModel> roleAssignViewModels = new List<RoleAssignViewModel>();//Model'den Liste şeklinde bir örnek aldık.
            foreach (var item in roles)//foreach roller içindeki her bir role tek tek bakacak.
            {
                RoleAssignViewModel model = new RoleAssignViewModel();
                model.RoleID = item.Id;//Roller Listesindeki item'in ID'sini model'in RoleID'sine ata.
                model.RoleName = item.Name;//Roller Listesindeki item'in Name'sini model'in RoleName'sine ata.
                model.RoleExist = userRoles.Contains(item.Name);//Kullanıcı Rolleri item'den gelen Name'i içeriyorsa true döner.
                roleAssignViewModels.Add(model);//
            }
            return View(roleAssignViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RoleAssignViewModel> roleAssignViewModel)
        {
            var userid = (int)TempData["userid"];
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userid);
            foreach (var item in roleAssignViewModel)
            {
                if (item.RoleExist)
                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
