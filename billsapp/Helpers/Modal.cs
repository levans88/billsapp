using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using billsapp.Enum;
using System.Reflection;
using System.ComponentModel;

namespace System.Web.Mvc {
    public class Modal {

        // Properties and default values
        public string ColorLevel = "header";
        public string Context = "primary";
        public string Title = "Are you sure?";
        public bool ShowSubTitle = false;
        public string SubTitle = "<h4>Primary</h4>";
        public string Icon = "\"fa fa-question-circle\"";
        public string Content = "";
        public bool ShowConfirmButton = true;
        public bool ShowDismissButton = true;
        public string ConfirmButtonText = "Confirm";
        public string DismissButtonText = "Cancel";
        public bool ShowNotification = false;
        public string NotificationText = "";

        // Modal class Constructor, color level is optional because it has a default value
        public Modal (ModalContext modalContext, ModalColorLevel modalColorLevel = ModalColorLevel.Header) {

            // Set Color Level
            if (modalColorLevel == ModalColorLevel.Default) {
                ColorLevel = "";
            }
            else if (modalColorLevel == ModalColorLevel.Full) {
                ColorLevel = "modal-full-color";
            }
            else {
                ColorLevel = "modal-header-color";
            }

            // Set context specific properties
            //
            // Primary
            if (modalContext == ModalContext.Primary) {
                Context = "primary";
                Title = "Are you sure?";
                SubTitle = "<h4>Primary</h4>";
                Icon = "\"fa fa-question-circle\"";
            }

            // Success
            if (modalContext == ModalContext.Success) {
                Context = "success";
                Title = "Success!";
                SubTitle = "<h4>Success</h4>";
                Icon = "\"fa fa-check\"";
            }

            // Info
            if (modalContext == ModalContext.Info) {
                Context = "info";
                Title = "Information";
                SubTitle = "<h4>Info</h4>";
                Icon = "\"fa fa-info-circle\"";
            }

            // Warning
            if (modalContext == ModalContext.Warning) {
                Context = "warning";
                Title = "Warning!";
                SubTitle = "<h4>Warning</h4>";
                Icon = "\"fa fa-warning\"";
            }

            // Danger
            if (modalContext == ModalContext.Danger) {
                Context = "danger";
                Title = "Danger!";
                SubTitle = "<h4>Danger</h4>";
                Icon = "\"fa fa-times-circle\"";
            }
        }
    }
}