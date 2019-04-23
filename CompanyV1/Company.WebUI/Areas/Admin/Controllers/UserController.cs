using Company.Business.Abstract;
using Company.Domain;
using Company.WebUI.Areas.Admin.Models;
using Company.WebUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Company.WebUI.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        IUserBusiness userBusiness = null;
        ICredentialBusiness credentialBusiness = null;

        public UserController(IUserBusiness userBusiness, ICredentialBusiness credentialBusiness = null)
        {
            this.userBusiness = userBusiness;
            this.credentialBusiness = credentialBusiness;
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = userBusiness.CheckLogin(loginViewModel.Username, loginViewModel.Password, isLoginAdmin: true);
                if (result > 0)
                {
                    var user = userBusiness.GetEmployeeByUserName(loginViewModel.Username);
                    Session[CommonConstants.CREDENTIAL_SESSION] = credentialBusiness.GetCredentials(loginViewModel.Username);
                    Session[CommonConstants.USER_SESSION] = new UserLogin { UserID = user.EmployeeID, UserName = user.Username , UserGroupID = user.UserGroupID };
                    return RedirectToAction("Index", "Home");
                }
                else if(result == -2)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn không có quyền đăng nhập");
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản hoặc mặt khẩu sai");
                }
            }
            return View(loginViewModel);
        }

        public ViewResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(SignUpViewModel signUpViewModel)
        {
            if (ModelState.IsValid)
            {
                if (userBusiness.CheckEmail(signUpViewModel.Email))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (userBusiness.CheckUserName(signUpViewModel.Username))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var employeeDomainModel = new EmployeeDomainModel
                    {
                        Username = signUpViewModel.Username,
                        Password = BCrypt.Net.BCrypt.HashPassword(signUpViewModel.Password),
                        Name = signUpViewModel.Name,
                        BirthDate = signUpViewModel.BirthDate.Value,
                        Address = signUpViewModel.Address,
                        Email = signUpViewModel.Email
                    };

                    if (userBusiness.AddUser(employeeDomainModel) > 0)
                    {
                        TempData["Success"] = "Đăng ký thành công";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công");
                    }
                }
            }
            return View(signUpViewModel);
        }

        public RedirectToRouteResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}