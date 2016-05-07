using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace billsapp.Models {
    public class ModalViewModel {
        private Modal _modal;

        public Modal Modal {
            get {
                return _modal;
            }
            set {
                _modal = value;

                // Named modal overrides (pass as parameters for constructor in the future...)
                if (_modal.Name == "siteInfoModal") {
                    _modal.ShowDismissButton = false;
                    _modal.ConfirmButtonText = "Ok";
                    _modal.SubTitle = "About";
                    _modal.ShowSubTitle = true;
                }
            }
        }
    }
}