using AutoMapper;
using Company.Business.Abstract;
using Company.Domain;
using Company.WebUI.Areas.Admin.Models;
using Company.WebUI.Common;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Company.WebUI.Areas.Admin.Controllers
{
    public class EquipmentController : BaseController
    {
        private IEquipmentBusiness equipmentBusiness;
        private ICategoryBusiness categoryBusiness;

        public EquipmentController(IEquipmentBusiness equipmentBusiness, ICategoryBusiness categoryBusiness)
        {
            this.equipmentBusiness = equipmentBusiness;
            this.categoryBusiness = categoryBusiness;
        }

        public void SetViewBag(int? ID = null)
        {
            ViewBag.CategoryID = ID.HasValue ? new SelectList(categoryBusiness.GetAllCategory(), "CategoryID", "Name", ID.Value) :
                new SelectList(categoryBusiness.GetAllCategory(), "CategoryID", "Name");

            ViewBag.Status = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Text = "Sẵn sàng", Value = (Convert.ToBoolean(1)).ToString() },
                    new SelectListItem { Text = "Chưa sẵn sàng", Value = (Convert.ToBoolean(0)).ToString() }
                }, "Value", "Text");
        }

        [HasCredential(RoleID = "VIEW_EQUIPMENT")]
        public ViewResult Index()
        {
            var equipmentDomainModel = equipmentBusiness.GetAllEquipment();
            var equipmentViewModel = new List<EquipmentViewModel>();
            Mapper.Map(equipmentDomainModel, equipmentViewModel);

            return View(equipmentViewModel);
        }

        [HasCredential(RoleID = "ADD_EQUIPMENT")]
        public ViewResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Exclude = "EquipmentID")]EquipmentViewModel equipmentViewModel)
        {
            if (ModelState.IsValid)
            {
                equipmentViewModel.Description = HttpUtility.HtmlEncode(equipmentViewModel.Description);
                var equipmentDomainModel = Mapper.Map<EquipmentViewModel, EquipmentDomainModel>(equipmentViewModel);
                if (equipmentBusiness.AddEquipment(equipmentDomainModel) > 0)
                {
                    SetAlert("Thêm thiết bị thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm thiết bị không thành công");
                }
            }
            SetViewBag();
            return View(equipmentViewModel);
        }

        [HasCredential(RoleID = "EDIT_EQUIPMENT")]
        public ViewResult Edit(int ID)
        {
            var equipmentDomainModel = equipmentBusiness.GetEquipmentByID(ID);
            var equipmentViewModel = Mapper.Map<EquipmentDomainModel, EquipmentViewModel>(equipmentDomainModel);
            SetViewBag(equipmentViewModel.CategoryID);

            return View(equipmentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Exclude = "Status")]EquipmentViewModel equipmentViewModel)
        {
            if (ModelState.IsValid)
            {
                equipmentViewModel.Description = HttpUtility.HtmlEncode(equipmentViewModel.Description);
                var equipmentDomainModel = Mapper.Map<EquipmentViewModel, EquipmentDomainModel>(equipmentViewModel);
                if (equipmentBusiness.UpdateEquipment(equipmentDomainModel) > 0)
                {
                    SetAlert("Cập nhật thiết bị thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thiết bị không thành công");
                }
            }

            return View(equipmentViewModel);
        }

        [HasCredential(RoleID = "DELETE_EQUIPMENT")]
        public RedirectToRouteResult Delete(int ID)
        {
            equipmentBusiness.DeleteEquipment(ID);
            SetAlert("Xóa thành công", "success");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(int ID)
        {
            return Json(new { status = equipmentBusiness.ChangeStatus(ID) }
            , JsonRequestBehavior.AllowGet);
        }
    }
}