using AutoMapper;
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
    public class EmployeeController : BaseController
    {
        private IEmployeeBusiness employeeBusiness;
        private IEquipmentBusiness equipmentBusiness;
        private IUserGroupBusiness userGroupBusiness;

        public EmployeeController(IEmployeeBusiness employeeBusiness, IUserGroupBusiness userGroupBusiness
            , IEquipmentBusiness equipmentBusiness)
        {
            this.employeeBusiness = employeeBusiness;
            this.equipmentBusiness = equipmentBusiness;
            this.userGroupBusiness = userGroupBusiness;
        }

        public void SetViewBag(int? equipmentID = null, string userGroupID = null)
        {
            ViewBag.EquipmentID = equipmentID.HasValue ?  new SelectList(equipmentBusiness.GetAllEquipment(), "EquipmentID", "Name", equipmentID.Value) : null;
            ViewBag.UserGroupID = !string.IsNullOrEmpty(userGroupID) ? new SelectList(userGroupBusiness.GetAllUserGroup(), "UserGroupID", "Name", userGroupID) : null;
        }
        
        [HasCredential(RoleID = "VIEW_EMPLOYEE")]
        public ViewResult Index()
        {
            var session = Session[CommonConstants.USER_SESSION] as UserLogin;
            IEnumerable<EmployeeDomainModel> employeeDomainModel = null;
            IEnumerable<EmployeeViewModel> employeeViewModel = null;
            if (session.UserGroupID.Equals(CommonConstants.ADMIN_GROUP))
            {
                employeeDomainModel = employeeBusiness.GetAllEmployee();
                employeeViewModel = new List<EmployeeViewModel>();
                Mapper.Map(employeeDomainModel, employeeViewModel);
                return View(employeeViewModel);
            }
            employeeDomainModel = employeeBusiness.GetAllEmployeeByID(session.UserID);
            employeeViewModel = new List<EmployeeViewModel>();
            Mapper.Map(employeeDomainModel, employeeViewModel);
            return View(employeeViewModel);

        }

        [HasCredential(RoleID = "EDIT_EMPLOYEE")]
        public ViewResult Edit(int ID)
        {
            var employeeDomainModel = employeeBusiness.GetEmployeeByID(ID);
            var employeeViewModel = Mapper.Map<EmployeeDomainModel, EmployeeViewModel>(employeeDomainModel);
            SetViewBag(employeeViewModel.EquipmentID, employeeViewModel.UserGroupID);

            return View(employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                var employeeDomainModel = Mapper.Map<EmployeeViewModel, EmployeeDomainModel>(employeeViewModel);
                if (employeeBusiness.UpdateEmployee(employeeDomainModel) > 0)
                {
                    SetAlert("Câp nhật nhân viên thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhâp nhân viên không thành công");
                }
            }
            SetViewBag(employeeViewModel.EquipmentID.Value, employeeViewModel.UserGroupID);
            return View(employeeViewModel);
        }

        [HasCredential(RoleID = "DELETE_EMPLOYEE")]
        public RedirectToRouteResult Delete(int ID)
        {
            employeeBusiness.DeleteEmployee(ID);
            SetAlert("Xóa nhân viên thành công", "success");

            return RedirectToAction("Index", "Employee");
        }
    }
}