using System.Web;
using System.Web.Mvc;
using System.Web.Security;

public class AccountController : Controller
{
    private readonly UserRepository _userRepository = new UserRepository();

    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Login(string username, string password)
    {
        var user = _userRepository.GetByUsernameAndPassword(username, password);
        if (user != null)
        {
            FormsAuthentication.SetAuthCookie(user.Username, false);
            return RedirectToAction("Index", "Home");
        }
        //Session["UserId"] = user.Id;
        ModelState.AddModelError("", "Invalid username or password");
        return View();
    }

    public ActionResult Logout()
    {
        Session.Abandon();
        Session.Clear();
        Session.RemoveAll();
        FormsAuthentication.SignOut();
        return RedirectToAction("Login");
    }
}
