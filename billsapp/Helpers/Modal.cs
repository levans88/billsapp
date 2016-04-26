using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using billsapp.Enum;
using System.ComponentModel;

namespace System.Web.Mvc {
    public class Modal {

        // Properties and default values
        public string Name = "";
        public string ColorLevel = "modal-header-color";
        public string Context = "primary";
        public string Title = "Are you sure?";
        public bool ShowSubTitle = false;
        public string SubTitle = "Primary";
        public string Icon = "fa-question-circle";
        public string Content = "";
        public bool ShowConfirmButton = true;
        public bool ShowDismissButton = true;
        public string ConfirmButtonText = "Confirm";
        public string DismissButtonText = "Cancel";
        public bool ShowNotification = false;
        public string NotificationText = "";

        // Modal class Constructor, color level is optional because it has a default value
        public Modal (string modalName, string modalContent, ModalContext modalContext, ModalColorLevel modalColorLevel = ModalColorLevel.Header) {
            
            // Modal name and content
            this.Name = modalName;
            this.Content = modalContent;

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
                SubTitle = "Primary";
                Icon = "fa-question-circle";
            }

            // Success
            if (modalContext == ModalContext.Success) {
                Context = "success";
                Title = "Success!";
                SubTitle = "Success";
                Icon = "fa-check";
            }

            // Info
            if (modalContext == ModalContext.Info) {
                Context = "info";
                Title = "Information";
                SubTitle = "Info";
                Icon = "fa-info-circle";
            }

            // Warning
            if (modalContext == ModalContext.Warning) {
                Context = "warning";
                Title = "Warning!";
                SubTitle = "Warning";
                Icon = "fa-warning";
            }

            // Danger
            if (modalContext == ModalContext.Danger) {
                Context = "danger";
                Title = "Danger!";
                SubTitle = "Danger";
                Icon = "fa-times-circle";
            }
        }
    }
}