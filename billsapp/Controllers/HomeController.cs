using billsapp.Enum;
using billsapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace billsapp.Controllers {
    public class HomeController : Controller {

        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CreateModal(string modalName, string modalContent, ModalContext modalContext, ModalColorLevel modalColorLevel = ModalColorLevel.Header) {

            // Create new modal with passed in name, content, etc.
            Modal m = new Modal(modalName, modalContent, modalContext, modalColorLevel);

            // Supply the new modal to the ViewModel
            ModalViewModel modalViewModel = new ModalViewModel() { Modal = m };

            return PartialView("_ModalAnim", modalViewModel);
        }
    }
}